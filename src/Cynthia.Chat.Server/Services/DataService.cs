using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cynthia.Chat.Server.Attributes;
using Cynthia.Chat.Server.Models;

namespace Cynthia.Chat.Server.Services
{
    [Singleton]
    internal class DataService : IDataService
    {
        private List<JsonData> ContextData = new List<JsonData>();
        public void AddData(JsonData data)
        {
            ContextData.Add(data);
        }
        public IEnumerable<JsonData> GetData(int count)
        {
            return ContextData.Skip(count);
        }
    }
}