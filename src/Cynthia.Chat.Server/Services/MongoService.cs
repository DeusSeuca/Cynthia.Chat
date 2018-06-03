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
        public IMongoClient Client { get; set; } = new MongoClient("mongodb://cynthia.ovyno.com:27017");
        public IDataService Data { get; set; }
        //public string Url { get; set; } = "mongodb://localhost:27017";
        private const string dataBaseName = "chat";
        private const string collectionName = "test";
        private int _strategy = 0;//缓存中的第几位开始,是数据库没有的数据
        public async void AutoSaveData(int minute = 1)
        {
            await Task.Delay(10000 * minute);//10秒
            SaveData();
        }
        public void InitData()
        {
            Data.AddData(GetEndData(50));
        }
        public void SaveData()
        {
            //测试用
            var url = "mongodb://cynthia.ovyno.com:27017";
            var client = new MongoClient(url);
            var db = client.GetDatabase("chat");
            var collection = db.GetCollection<JsonData>("test");
            //var collection = Client.GetDatabase(dataBaseName).GetMongoCollection<JsonData>(collectionName);
            Data.GetData(0).Skip(_strategy).ForAll(x => collection.InsertOne(x));
            collection.InsertOne(new JsonData { Name = "测试数据", Content = "如果数据库出现这条数据,代表上面的语句错误", Time = DateTime.Now, Id = Guid.NewGuid() });
            _strategy = Data.Count;
        }
        public IEnumerable<JsonData> GetEndData(int count)
        {
            //获得默认的count条数据
            //测试用
            var url = "mongodb://cynthia.ovyno.com:27017";
            var client = new MongoClient(url);
            var db = client.GetDatabase("chat");
            var collection = db.GetCollection<JsonData>("test");
            //var collection = Client.GetDatabase(dataBaseName).GetMongoCollection<JsonData>(collectionName);
            var data = collection.AsQueryable<JsonData>().OrderBy(x => x.Time).Reverse().Take(count).Reverse().AsEnumerable();
            _strategy = data.Count();
            return data;
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