using System;
using System.Linq;
using MongoDB.Driver;

namespace DatabaseTest.MongoDB
{
    internal partial class MongoRepository<TModel> : MongoRepository
    {
        private IQueryable<TModel> queryable => _collection.AsQueryable();

        private readonly IMongoCollection<TModel> _collection;

        private TModel AutoId(TModel value) => AutoId(value, null);

        private TModel AutoId(TModel value, string id)
        {
            if (value.Id == null || value.Id == string.Empty)
            {
                value.Id = id ?? Guid.NewGuid().ToString();
            }
            return value;
        }

        public MongoRepository(MongoDatabase database, string name, IMongoCollection<TModel> collection) : base(database, name)
        {
            _collection = collection;
        }
    }
}