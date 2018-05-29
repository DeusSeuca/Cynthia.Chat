using System.Collections.Generic;
using Cynthia.Test.Chat.Models;

namespace Cynthia.Test.Chat.Services
{
    public interface IDataService
    {
        void AddData(JsonData data);
        IEnumerable<JsonData> GetData(int count);
    }
}