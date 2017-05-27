using MongoDB.Driver;

namespace Brandbank.Api.Logging.MongoDb
{
    public class Context<T>
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public Context(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            _collectionName = collectionName;
        }

        public IMongoCollection<MongoDownloadItem<T>> LogData => _database.GetCollection<MongoDownloadItem<T>>(_collectionName);
    }
}
