using System.Collections.Generic;
using System.Threading.Tasks;
using Cynthia.Chat.Common.Models;
using Newtonsoft.Json;

namespace Cynthia.Chat.Client
{
    public class DataProcessing
    {
        public string Url { get; set; } = "http://localhost:5000/api/chat/";
        public async void SendData(string name, string context)
        {
            await Json.PostJson<JsonData>(Url,new JsonData(name,context));
        }
        public async Task<IEnumerable<JsonData>> GetData(int count)
        {
            return await Json.GetJson<IEnumerable<JsonData>>(Url + count);
        }
    }
}