using System;
using MongoDB.Driver;
using Cynthia.Chat.Common;
using System.Linq;
using System.Collections.Generic;
using Cynthia.Chat.Server.Attributes;

namespace Cynthia.Chat.MongoDB
{
    [Singleton]
    internal partial class MongoDatabaseService
    {
        private IMongoClient _mongoClient { get; set; }
        private IEnumerable<IDatabase> databases => _mongoClient.ListDatabases().ToEnumerable().Select(data => this[data["name"].ToString()]);
        public MongoDatabaseService(IMongoClient mongoclient) => _mongoClient = mongoclient;

        public override bool Equals(object obj) => obj is MongoDatabaseService service && service._mongoClient == _mongoClient;
        public override int GetHashCode() => base.GetHashCode();
    }
}
