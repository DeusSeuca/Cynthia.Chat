using System.Collections.Generic;
using Cynthia.Chat.Common.Models;

namespace Cynthia.Chat.Server.Services
{
    public interface IDataService
    {
        void AddData(JsonData data);
        IEnumerable<JsonData> GetData(int count);
    }
}