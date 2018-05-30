using System;
using Newtonsoft.Json;

namespace Cynthia.Chat.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            DataProcessing client = new DataProcessing();
            client.SendData("格子","我才不是傲娇哼呢");
            Console.WriteLine(JsonConvert.SerializeObject(client.GetData(0)));
        }
    }
}
