using System.Collections.Generic;
using WebTest1.Models;

namespace WebTest1.Services
{
    public interface IDataService
    {
        void AddData(JsonData data);
        IEnumerable<JsonData> GetData(int count);
    }
}