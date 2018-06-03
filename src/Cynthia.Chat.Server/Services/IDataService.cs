using System.Collections.Generic;
using Cynthia.Chat.Common.Models;

namespace Cynthia.Chat.Server.Services
{
    public interface IDataService
    {
        void AddData(JsonData data);
        void AddData(IEnumerable<JsonData> data);
        int Count { get; }
        IEnumerable<JsonData> GetData(int count);
    }
}