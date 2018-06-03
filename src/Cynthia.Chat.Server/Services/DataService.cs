using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cynthia.Chat.Server.Attributes;
using Cynthia.Chat.Common.Models;
using Alsein.Utilities;

namespace Cynthia.Chat.Server.Services
{
    [Singleton]
    internal class DataService : IDataService
    {
        private List<JsonData> ContextData = new List<JsonData>();
        public int Count
        {
            get
            {
                return ContextData.Count;
            }
        }
        public void AddData(JsonData data)
        {
            ContextData.Add(data);
        }
        public void AddData(IEnumerable<JsonData> data)
        {
            data.ForAll(x => ContextData.Add(x));
        }
        public IEnumerable<JsonData> GetData(int count)
        {
            return ContextData.Skip(count);
        }
    }
}