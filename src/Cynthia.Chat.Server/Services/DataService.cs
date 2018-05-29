using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cynthia.Test.Chat.Attributes;
using Cynthia.Test.Chat.Models;

namespace Cynthia.Test.Chat.Services
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