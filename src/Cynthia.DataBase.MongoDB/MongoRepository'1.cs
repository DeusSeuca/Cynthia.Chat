using System;
using System.Linq;
using MongoDB.Driver;

namespace Cynthia.DataBase.MongoDB
{
    internal partial class MongoRepository<TModel> : MongoRepository
    {
        private IQueryable<TModel> queryable => _collection.AsQueryable<TModel>();
        private IMongoCollection<TModel> _collection;
        private TModel AutoId(TModel item) => AutoId(item, null);
        private TModel AutoId(TModel item, string id)
        {
            if (item.Id == null || item.Id == string.Empty)
            {
                item.Id = id ?? Guid.NewGuid().ToString();
            }
            return item;
        }
        public MongoRepository(MongoDatabase database, string name, IMongoCollection<TModel> collection) : base(database, name)
        {
            _collection = collection;
        }
    }
}