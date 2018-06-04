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
        public async void AutoSaveData(int minute = 1)
        {
            while (true)
            {
                await Task.Delay(10000 * minute);//为了测试设置为10秒
                SaveData();
            }
        }
        public void InitData()
        {
            Data.AddData(GetEndData(50));
        }
        public void SaveData()
        {
            var collection = Client.GetDatabase(dataBaseName).GetMongoCollection<JsonData>(collectionName);
            Data.GetData(0).Skip(_strategy).ForAll(x => collection.InsertOne(x));
            _strategy = Data.Count;
        }
        public IEnumerable<JsonData> GetEndData(int count)
        {
            var collection = Client.GetDatabase(dataBaseName).GetMongoCollection<JsonData>(collectionName);
            var data = collection.AsQueryable<JsonData>().OrderByDescending(x => x.Time).Take(count).OrderBy(x => x.Time).AsEnumerable();
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