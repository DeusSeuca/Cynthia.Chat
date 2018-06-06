using System;
using Newtonsoft.Json;
using Cynthia.Chat.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alsein.Utilities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Cynthia.Chat.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            /* var url = "mongodb://cynthia.ovyno.com:27017";
             var cl = new MongoClient(url);
             var db = cl.GetDatabase("chat");
             var c = db.GetCollection<JsonData>("test");
             43.To(500).ForAll(x => c.InsertOne(new JsonData { Name = "怜昕", Content = "测试用数据,数据编号:" + x, Time = DateTime.Now, Id = Guid.NewGuid() }));
             c.AsQueryable().AsEnumerable<JsonData>().Select(x => $"-------------------------\n{x.Name}    \n{x.Content}").ForAll(Console.WriteLine);
             Console.ReadKey();*/
            string userName;
            string context;
            int count = 0;
            Console.WriteLine("请输入在聊天室的名称~:");
            userName = Console.ReadLine();
            DataProcessing client = new DataProcessing();
            client.Url = "http://cynthia.ovyno.com/api/chat/";
            Test(count, client);
            while (true)
            {
                context = Console.ReadLine();
                client.SendData(userName, context);
            }
        }
        public static async void Test(int count, DataProcessing client)
        {
            while (true)
            {
                count += PutData(await client.GetData(count));
                await Task.Delay(1000);
            }
        }
        public static int PutData(IEnumerable<JsonData> data)
        {
            return data.Select(d => $"{d.Name}  {d.Time}\n {d.Content}\n---------------------------------").ForAll(Console.WriteLine).Count();
        }
    }
}
