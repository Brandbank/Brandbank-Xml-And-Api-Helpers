using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Api.Logging
{
    public class DownloadLogRepositoryLogger<T> : IDownloadLog<T>
    {
        private readonly ILogger<IDownloadLog<T>> _logger;
        private readonly IDownloadLog<T> _downloadLog;

        public DownloadLogRepositoryLogger(ILogger<IDownloadLog<T>> logger, IDownloadLog<T> downloadLog)
        {
            _logger = logger;
            _downloadLog = downloadLog;
        }

        public IEnumerable<MongoDownloadItem<T>> Get(Expression<Func<MongoDownloadItem<T>, bool>> predicate)
        {
            _logger.LogDebug($"Getting download log item [{predicate.Body.ToString()}]");
            try
            {
                var response = _downloadLog.Get(predicate);
                if (response != null)
                    _logger.LogDebug($"Got {response.Count()} download log item(s) [{predicate.Body.ToString()}]");
                else
                    _logger.LogDebug($"Got download log item [{predicate.Body.ToString()}] returned 0 results");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Getting download log item failed [{predicate.Body.ToString()}]");
                throw;
            }
        }

        public void Add(MongoDownloadItem<T> data)
        {
            _logger.LogDebug($"Adding download log item {data.ProductCode}");
            try
            {
                _downloadLog.Add(data);
                _logger.LogDebug($"Added download log item {data.ProductCode}");
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Adding download log item failed {data.ProductCode}");
                throw;
            }
        }

        public void AddOrUpdate(Expression<Func<MongoDownloadItem<T>, bool>> predicate, MongoDownloadItem<T> data)
        {
            _logger.LogDebug($"Adding or updating download log item {data.ProductCode} [{predicate.Body.ToString()}]");
            try
            {
                _downloadLog.AddOrUpdate(predicate, data);
                _logger.LogDebug($"Adding or updating download log item {data.ProductCode} [{predicate.Body.ToString()}]");
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Adding or updating download log item failed {data.ProductCode} [{predicate.Body.ToString()}]");
                throw;
            }
        }

        public void Update(Expression<Func<MongoDownloadItem<T>, bool>> predicate, Guid receiptId, bool brandbankSuccessfullyImported, string messageText, string messageType)
        {
            _logger.LogDebug($"Updating download log item {messageType} - {messageText} - {receiptId.ToString()} [{predicate.Body.ToString()}]");
            try
            {
                _downloadLog.Update(predicate, receiptId, brandbankSuccessfullyImported, messageText, messageType);
                _logger.LogDebug($"Updated download log item {messageType} - {messageText} - {receiptId.ToString()} [{predicate.Body.ToString()}]");
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Updating download log item failed {messageType} - {messageText} - {receiptId.ToString()} [{predicate.Body.ToString()}]");
                throw;
            }
        }

        public void Update<TField>(Expression<Func<MongoDownloadItem<T>, bool>> predicate, Expression<Func<MongoDownloadItem<T>, TField>> field, TField value)
        {
            _logger.LogDebug($"Updating download log item [{predicate.Body.ToString()}] [{value}]");
            try
            {
                _downloadLog.Update(predicate, field, value);
                _logger.LogDebug($"Updated download log item [{predicate.Body.ToString()}] [{value}]");
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Updating download log item failed [{predicate.Body.ToString()}] [{value}]");
                throw;
            }
        }
    }
}
