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
    internal class MessageService : IMessagesService
    {
        private List<ChatMessage> ContextData = new List<ChatMessage>();
        public int Count
        {
            get
            {
                return ContextData.Count;
            }
        }
        public void AddMessage(ChatMessage data)
        {
            ContextData.Add(data);
        }
        public void AddMessage(IEnumerable<ChatMessage> data)
        {
            data.ForAll(x => ContextData.Add(x));
        }
        public IEnumerable<ChatMessage> GetMessage(int count)
        {
            return ContextData.Skip(count);
        }
    }
}