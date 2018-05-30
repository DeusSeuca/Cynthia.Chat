using System;
using Newtonsoft.Json;
using Cynthia.Chat.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alsein.Utilities;

namespace Cynthia.Chat.Client
{
    class Program
    {
        static void Main(string[] args)
        {
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
