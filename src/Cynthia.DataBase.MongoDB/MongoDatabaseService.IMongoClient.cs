using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;

namespace Cynthia.DataBase.MongoDB
{
    internal partial class MongoDatabaseService : IMongoClient
    {
        public ICluster Cluster => _mongoclient.Cluster;

        public MongoClientSettings Settings => _mongoclient.Settings;

        public void DropDatabase(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            _mongoclient.DropDatabase(name, cancellationToken);
        }

        public void DropDatabase(IClientSessionHandle session, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            _mongoclient.DropDatabase(session, name, cancellationToken);
        }

        public Task DropDatabaseAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mongoclient.DropDatabaseAsync(name, cancellationToken);
        }

        public Task DropDatabaseAsync(IClientSessionHandle session, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mongoclient.DropDatabaseAsync(session, name, cancellationToken);
        }

        public IMongoDatabase GetDatabase(string name, MongoDatabaseSettings settings = null)
        {
            return _mongoclient.GetDatabase(name, settings);
        }

        public IAsyncCursor<BsonDocument> ListDatabases(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mongoclient.ListDatabases(cancellationToken);
        }

        public IAsyncCursor<BsonDocument> ListDatabases(IClientSessionHandle session, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mongoclient.ListDatabases(session, cancellationToken);
        }

        public Task<IAsyncCursor<BsonDocument>> ListDatabasesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mongoclient.ListDatabasesAsync(cancellationToken);
        }

        public Task<IAsyncCursor<BsonDocument>> ListDatabasesAsync(IClientSessionHandle session, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mongoclient.ListDatabasesAsync(session, cancellationToken);
        }

        public IClientSessionHandle StartSession(ClientSessionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mongoclient.StartSession(options, cancellationToken);
        }

        public Task<IClientSessionHandle> StartSessionAsync(ClientSessionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mongoclient.StartSessionAsync(options, cancellationToken);
        }

        public IMongoClient WithReadConcern(ReadConcern readConcern)
        {
            return _mongoclient.WithReadConcern(readConcern);
        }

        public IMongoClient WithReadPreference(ReadPreference readPreference)
        {
            return _mongoclient.WithReadPreference(readPreference);
        }

        public IMongoClient WithWriteConcern(WriteConcern writeConcern)
        {
            return _mongoclient.WithWriteConcern(writeConcern);
        }
    }
}