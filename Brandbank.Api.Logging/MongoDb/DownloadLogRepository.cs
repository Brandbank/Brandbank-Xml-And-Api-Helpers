using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Brandbank.Api.Logging.MongoDb
{
    public class MongoLogRepository<T> : IDownloadLog<T>
    {
        private readonly Context<T> _ctx;

        public MongoLogRepository(string connectionString, string databaseName, string collectionName)
        {
            _ctx = new Context<T>(connectionString, databaseName, collectionName);
        }

        public IEnumerable<MongoDownloadItem<T>> Get(Expression<Func<MongoDownloadItem<T>, bool>> predicate)
        {
            var filter = Builders<MongoDownloadItem<T>>.Filter.Where(predicate);
            return _ctx.LogData.Find(filter).ToList();
        }

        public void Add(MongoDownloadItem<T> data)
        {
            _ctx.LogData.InsertOne(data);
        }

        public void AddOrUpdate(Expression<Func<MongoDownloadItem<T>, bool>> predicate, MongoDownloadItem<T> data)
        {
            _ctx.LogData.ReplaceOne(predicate, data, new UpdateOptions
            {
                IsUpsert = true
            });
        }

        public void Update<TIn>(Expression<Func<MongoDownloadItem<T>, bool>> predicate, Expression<Func<MongoDownloadItem<T>, TIn>> field, TIn value)
        {
            var filter = Builders<MongoDownloadItem<T>>.Filter.Where(predicate);
            var update = Builders<MongoDownloadItem<T>>.Update.Set(field, value);
            _ctx.LogData.UpdateMany(filter, update);
        }

        public void Update(Expression<Func<MongoDownloadItem<T>, bool>> predicate, Guid receiptId, bool brandbankSuccessfullyImported, string messageText, string messageType)
        {
            var filter = Builders<MongoDownloadItem<T>>.Filter.Where(predicate);
            var update = Builders<MongoDownloadItem<T>>.Update.Combine(
                Builders<MongoDownloadItem<T>>.Update.Set(f => f.ReceiptId, receiptId),
                Builders<MongoDownloadItem<T>>.Update.Set(f => f.UploadedToBrandbank, brandbankSuccessfullyImported),
                Builders<MongoDownloadItem<T>>.Update.Set(f => f.MessageText, messageText),
                Builders<MongoDownloadItem<T>>.Update.Set(f => f.MessageType, messageType),
                Builders<MongoDownloadItem<T>>.Update.Set(f => f.ModifiedDate, DateTime.UtcNow)
                );
            _ctx.LogData.UpdateOne(filter, update);
        }
    }
}
