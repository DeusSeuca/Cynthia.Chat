using System.Collections;
using System.Collections.Generic;
using Cynthia.DataBase.Common;

namespace Cynthia.DataBase.MongoDB
{
    internal partial class MongoDatabaseService : IDatabaseService
    {
        public IDatabase this[string name] => new MongoDatabase(_mongoclient.GetDatabase(name));

        public IEnumerator<IDatabase> GetEnumerator() => databases.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => databases.GetEnumerator();
    }
}