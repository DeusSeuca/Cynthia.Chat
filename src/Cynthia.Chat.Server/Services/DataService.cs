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
        private List<ChatMessage> ContextData = new List<ChatMessage>();
        public int Count
        {
            get
            {
                return ContextData.Count;
            }
        }
        public void AddData(ChatMessage data)
        {
            ContextData.Add(data);
        }
        public void AddData(IEnumerable<ChatMessage> data)
        {
            data.ForAll(x => ContextData.Add(x));
        }
        public IEnumerable<ChatMessage> GetData(int count)
        {
            return ContextData.Skip(count);
        }
    }
}