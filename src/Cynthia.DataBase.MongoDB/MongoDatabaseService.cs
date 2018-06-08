using System;
using MongoDB.Driver;
using Cynthia.DataBase.Common;
using System.Linq;
using System.Collections.Generic;

namespace Cynthia.DataBase.MongoDB
{
    internal partial class MongoDatabaseService
    {
        private IMongoClient _mongoclient { get; set; }
        private IEnumerable<IDatabase> databases => _mongoclient.ListDatabases().ToEnumerable().Select(data => this[data["name"].ToString()]);
        public MongoDatabaseService(IMongoClient mongoclient) => _mongoclient = mongoclient;

        public override bool Equals(object obj) => obj is MongoDatabaseService service && service._mongoclient == _mongoclient;
        public override int GetHashCode() => base.GetHashCode();
    }
}
