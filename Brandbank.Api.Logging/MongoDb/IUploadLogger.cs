using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Brandbank.Api.UploadData;

namespace Brandbank.Api.Logging.MongoDb
{
    public interface IUploadLogger<T>
    {
        void AddItem(T product, string productCode, string batchId);
        IEnumerable<MongoDownloadItem<T>> GetItems(string batchId);
        void NofityFailures(UploadResponse uploadResponse, string batchDirectory, string batchId);
        void UpdateAcknowledgeStatus(Expression<Func<MongoDownloadItem<T>, bool>> predicate);
        void UpdateBrandbankImportStatus(UploadResponse uploadResponse, string batchId);
    }
}