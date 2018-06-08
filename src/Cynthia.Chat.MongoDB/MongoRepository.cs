namespace DatabaseTest.MongoDB
{
    internal class MongoRepository : IRepository
    {
        public string Name { get; }

        public IDatabase Database { get; }

        public IRepository<TModel> As<TModel>() where TModel : ModelBase => Database.GetRepository<TModel>(Name);

        public MongoRepository(MongoDatabase database, string name)
        {
            Database = database;
            Name = name;
        }

        public override bool Equals(object obj) => obj is MongoRepository repository && Name == repository.Name && Database == repository.Database;

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => Name;
    }
}