using System.Linq;
using System.Threading.Tasks;
using Cynthia.Chat.Server.Attributes;
using Alsein.Utilities;
using MongoDB.Driver;
using MongoDB.Bson;
using Cynthia.Chat.Common.Models;
using System.Collections.Generic;
using System;

namespace Cynthia.Chat.Server.Services
{
    [Singleton]
    public class MongoService
    {
        public IMongoClient Client { get; set; }
        public IDataService Data { get; set; }
        private const string dataBaseName = "chat";
        private const string collectionName = "test";
        private int _strategy = 0;//缓存中的第几位开始,是数据库没有的数据
        public async void AutoSaveData(int minute = 10)
        {
            while (true)
            {
                await Task.Delay(60000 * minute);//为了测试设置为10分钟
                SaveData();
            }
        }
        public void InitData(int initnum = 60)
        {
            //服务开始时,先从数据库取出末尾60条数据
            Data.AddData(GetEndData(initnum));
        }
        public void SaveData()
        {
            //将缓存中多余的数据存入数据库,用_strategy做标识并更新_strategy的值
            Data.GetData(0).Skip(_strategy).To(DataToMongo);
            _strategy = Data.Count;
        }
        public void DataToMongo(IEnumerable<JsonData> data)
        {
            //将集合中所有元素添加进数据库
            data.ForAll(x => x.To(DataToMongo));
        }
        public void DataToMongo(JsonData data)
        {
            //将集合中所有元素添加进数据库
            var collection = GetCollection();
            collection.InsertOne(data);
        }
        public IMongoCollection<JsonData> GetCollection()
        {
            //获得数据库集合
            return Client.GetDatabase(dataBaseName).GetMongoCollection<JsonData>(collectionName);
        }
        public IEnumerable<JsonData> GetEndData(int count)
        {
            //获得数据库末尾的count条数据
            var collection = GetCollection();
            var data = collection.AsQueryable<JsonData>().OrderByDescending(x => x.Time).Take(count).OrderBy(x => x.Time).AsEnumerable();
            _strategy = data.Count();
            return data;
        }
        public int GetDataCount()
        {
            //获得数据库中总共的数据数量
            var collection = GetCollection();
            return collection.AsQueryable<JsonData>().Count() + Data.Count - _strategy;
        }
        public IEnumerable<JsonData> GetPageData(int page, int count = 60)
        {
            //获得某一页的数据  (每个的数量,和第几页  默认一页60条数据
            var collection = GetCollection();
            var pagecount = GetPageNum(count);
            return collection.AsQueryable<JsonData>().OrderBy(x => x.Time).Skip(page * count).Take(count).AsEnumerable();
        }
        public int GetPageNum(int count = 60)
        {
            //获得总共的页数  默认一页60条数据
            var datacount = GetDataCount();
            if (datacount % count != 0)
            {
                return datacount / count + 1;
            }
            return datacount / count;
        }
    }
    public static class MongoExtension
    {
        public static IMongoDatabase GetMongoDatabase(this IMongoClient client, string dataBaseName)
        {
            return client.GetDatabase(dataBaseName);
        }
        public static IMongoCollection<T> GetMongoCollection<T>(this IMongoDatabase db, string collectionName)
        {
            return db.GetCollection<T>(collectionName);
        }
    }
}