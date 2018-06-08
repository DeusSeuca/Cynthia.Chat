using Cynthia.DataBase.Common;

namespace Cynthia.DataBase.MongoDB
{
    internal class MongoRepository : IRepository
    {
        public string Name { get; }

        public IDatabase Database { get; }

        public MongoRepository(MongoDatabase database, string name)
        {
            Name = name;
            Database = database;
        }
        public IRepository<TModel> As<TModel>() where TModel : ModelBase => Database.GetRepository<TModel>(Name);
        public override bool Equals(object obj) => obj is MongoRepository repositroy && repositroy.Name == Name && repositroy.Database == Database;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => Name;
    }
}