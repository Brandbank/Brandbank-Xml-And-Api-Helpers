using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Brandbank.Api.Logging
{
    public interface IDownloadLog<T>
    {
        IEnumerable<MongoDownloadItem<T>> Get(Expression<Func<MongoDownloadItem<T>, bool>> predicate);
        void Add(MongoDownloadItem<T> data);
        void AddOrUpdate(Expression<Func<MongoDownloadItem<T>, bool>> predicate, MongoDownloadItem<T> data);
        void Update<TField>(Expression<Func<MongoDownloadItem<T>, bool>> predicate, Expression<Func<MongoDownloadItem<T>, TField>> field, TField value);
        void Update(Expression<Func<MongoDownloadItem<T>, bool>> predicate, Guid receiptId, bool brandbankSuccessfullyImported, string messageText, string messageType);
    }
}
