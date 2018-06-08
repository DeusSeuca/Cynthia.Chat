using System.Collections.Generic;
using Cynthia.Chat.Common.Models;

namespace Cynthia.Chat.Server.Services
{
    public interface IMessagesService
    {
        void AddMessage(ChatMessage data);
        void AddMessage(IEnumerable<ChatMessage> data);
        int Count { get; }
        IEnumerable<ChatMessage> GetMessage(int count);
    }
}