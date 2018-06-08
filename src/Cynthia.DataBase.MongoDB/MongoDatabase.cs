using MongoDB.Driver;

namespace Cynthia.DataBase.MongoDB
{
    internal partial class MongoDatabase
    {
        public MongoDatabase(IMongoDatabase database) => _database = database;
        private IMongoDatabase _database { get; set; }
        public override bool Equals(object obj) => obj is MongoDatabase database && database._database == _database;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => _database.DatabaseNamespace.DatabaseName;
    }
}