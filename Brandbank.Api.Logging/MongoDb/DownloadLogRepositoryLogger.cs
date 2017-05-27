using Brandbank.Xml.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Brandbank.Api.Logging.MongoDb
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
            _logger.LogDebug($"Getting download log item [{predicate.Body}]");
            try
            {
                var response = _downloadLog.Get(predicate);
                _logger.LogDebug(response != null
                                     ? $"Got {response.Count()} download log item(s) [{predicate.Body}]"
                                     : $"Got download log item [{predicate.Body}] returned 0 results");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting download log item failed [{predicate.Body}]: {e.Message}");
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
                _logger.LogError($"Adding download log item failed {data.ProductCode}: {e.Message}");
                throw;
            }
        }

        public void AddOrUpdate(Expression<Func<MongoDownloadItem<T>, bool>> predicate, MongoDownloadItem<T> data)
        {
            _logger.LogDebug($"Adding or updating download log item {data.ProductCode} [{predicate.Body}]");
            try
            {
                _downloadLog.AddOrUpdate(predicate, data);
                _logger.LogDebug($"Adding or updating download log item {data.ProductCode} [{predicate.Body}]");
            }
            catch (Exception e)
            {
                _logger.LogError($"Adding or updating download log item failed {data.ProductCode} [{predicate.Body}]: {e.Message}");
                throw;
            }
        }

        public void Update(Expression<Func<MongoDownloadItem<T>, bool>> predicate, Guid receiptId, bool brandbankSuccessfullyImported, string messageText, string messageType)
        {
            _logger.LogDebug($"Updating download log item {messageType} - {messageText} - {receiptId} [{predicate.Body}]");
            try
            {
                _downloadLog.Update(predicate, receiptId, brandbankSuccessfullyImported, messageText, messageType);
                _logger.LogDebug($"Updated download log item {messageType} - {messageText} - {receiptId} [{predicate.Body}]");
            }
            catch (Exception e)
            {
                _logger.LogError($"Updating download log item failed {messageType} - {messageText} - {receiptId} [{predicate.Body}]: {e.Message}");
                throw;
            }
        }

        public void Update<TField>(Expression<Func<MongoDownloadItem<T>, bool>> predicate, Expression<Func<MongoDownloadItem<T>, TField>> field, TField value)
        {
            _logger.LogDebug($"Updating download log item [{predicate.Body}] [{value}]");
            try
            {
                _downloadLog.Update(predicate, field, value);
                _logger.LogDebug($"Updated download log item [{predicate.Body}] [{value}]");
            }
            catch (Exception e)
            {
                _logger.LogError($"Updating download log item failed [{predicate.Body}] [{value}]: {e.Message}");
                throw;
            }
        }
    }
}
