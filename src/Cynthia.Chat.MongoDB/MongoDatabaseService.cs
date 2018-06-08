using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace DatabaseTest.MongoDB
{
    internal partial class MongoDatabaseService
    {
        private IMongoClient _mongoClient { get; set; }

        private IEnumerable<IDatabase> databases => _mongoClient.ListDatabases().ToEnumerable().Select(data => this[data["name"].ToString()]);

        public override bool Equals(object obj) => obj is MongoDatabaseService service && _mongoClient == service._mongoClient;

        public override int GetHashCode() => base.GetHashCode();

        public MongoDatabaseService(IMongoClient mongoClient) => _mongoClient = mongoClient;
    }
}