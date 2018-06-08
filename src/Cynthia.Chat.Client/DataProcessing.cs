using System.Collections.Generic;
using System.Threading.Tasks;
using Cynthia.Chat.Common.Models;
using Newtonsoft.Json;
using System;

namespace Cynthia.Chat.Client
{
    public class DataProcessing
    {
        public string Url { get; set; } = "http://localhost:5000/api/chat/";
        public async void SendData(string name, string context)
        {
            await Json.PostJson<ChatMessage>(Url, new ChatMessage() { Name = name, Content = context, Id = Guid.NewGuid().ToString() });
        }
        public async Task<IEnumerable<ChatMessage>> GetData(int count)
        {
            return await Json.GetJson<IEnumerable<ChatMessage>>(Url + count);
        }
    }
}