using System.Collections.Generic;
using Cynthia.Chat.Common.Models;

namespace Cynthia.Chat.Server.Services
{
    public interface IDataService
    {
        void AddData(ChatMessage data);
        void AddData(IEnumerable<ChatMessage> data);
        int Count { get; }
        IEnumerable<ChatMessage> GetData(int count);
    }
}