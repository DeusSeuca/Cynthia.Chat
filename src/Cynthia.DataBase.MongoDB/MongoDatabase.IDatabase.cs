using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cynthia.DataBase.Common;
using MongoDB.Driver;

namespace Cynthia.DataBase.MongoDB
{
    internal partial class MongoDatabase : IDatabase
    {
        public string Name => _database.DatabaseNamespace.DatabaseName;

        public IEnumerator<IRepository> GetEnumerator() => _database.ListCollections().ToEnumerable().Select(data => new MongoRepository(this, data["name"].ToString())).GetEnumerator();

        public IRepository GetRepository(string name) => new MongoRepository(this, Name);

        public IRepository<TModel> GetRepository<TModel>(string name) where TModel : ModelBase => new MongoRepository<TModel>(this, name, _database.GetCollection<TModel>(name));

        public IRepository<TModel> GetRepository<TModel>() where TModel : ModelBase => GetRepository<TModel>(typeof(TModel).Name);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}