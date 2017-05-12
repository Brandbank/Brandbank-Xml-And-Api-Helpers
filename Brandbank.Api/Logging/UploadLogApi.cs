using Brandbank.Api.NotifyClient;
using Brandbank.Api.UploadData;
using Brandbank.Xml.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Brandbank.Api.UploadData.Message;
using static Brandbank.Api.UploadData.UploadResponse;

namespace Brandbank.Api.Logging
{
    public class UploadLogger<T> : IUploadLogger<T>
    {
        private readonly IDownloadLog<T> _log;
        private readonly INotifyClient _notifyClient;

        public UploadLogger(IDownloadLog<T> log, INotifyClient notifyClient)
        {
            if (log == null) throw new ArgumentNullException("log");
            if (notifyClient == null) throw new ArgumentNullException("notifyClient");

            _log = log;
            _notifyClient = notifyClient;
        }

        public void UpdateAcknowledgeStatus(Expression<Func<MongoDownloadItem<T>, bool>> predicate)
        {
            _log.Update(
                predicate: predicate,
                field: l => l.Acknowledged,
                value: true);
        }

        public void UpdateBrandbankImportStatus(UploadResponse uploadResponse, string batchId)
        {
            foreach (var message in uploadResponse.Messages)
            {
                _log.Update(
                    predicate: p => p.ProductCode.Equals(message.Text.GetEan().PadLeft(14, '0')) & p.BatchId.Equals(batchId),
                    receiptId: uploadResponse.ReceiptId,
                    brandbankSuccessfullyImported: message.MessageType.Equals(MessageTypes.Information),
                    messageText: message.Text,
                    messageType: message.MessageType.ToString()
                    );
            }
        }

        public void AddItem(T product, string productCode, string batchId)
        {
            _log.Add(new MongoDownloadItem<T>
            {
                ProductCode = productCode.PadLeft(14, '0'),
                Acknowledged = false,
                UploadedToBrandbank = false,
                MessageText = string.Empty,
                MessageType = string.Empty,
                Data = product,
                BatchId = batchId,
                CreatedDate = DateTime.UtcNow
            });
        }

        public IEnumerable<MongoDownloadItem<T>> GetItems(string batchId)
        {
            return _log.Get(p => !p.Acknowledged & p.UploadedToBrandbank & p.BatchId.Equals(batchId));
        }

        public void NofityFailures(UploadResponse uploadResponse, string batchDirectory, string batchId)
        {
            if (!uploadResponse.Status.Equals(UploadStatuses.Success))
            {
                var sb = new StringBuilder()
                    .AppendLine($"BatchDirectory: {batchDirectory}")
                    .AppendLine($"BatchId: {batchId}")
                    .AppendLine($"---------Brandbank---------")
                    .AppendLine($"ReceiptId: {uploadResponse.ReceiptId}")
                    .AppendLine($"Timestamp: {uploadResponse.Timestamp}")
                    .AppendLine($"Status: {uploadResponse.Status.ToString()}")
                    .AppendLine($"FilesReceivedCount: {uploadResponse.FilesReceivedCount}")
                    .AppendLine($"---------Messages----------")
                    .AppendLine($"Messages:");
                foreach (var message in uploadResponse.Messages)
                {
                    sb.AppendLine($"Code: {message.Code}, MessageType: {message.MessageType}, Text: {message.Text}");
                }
                _notifyClient.Notify("Label Insight M2M import failures", sb.ToString());
            }
        }
    }
}
