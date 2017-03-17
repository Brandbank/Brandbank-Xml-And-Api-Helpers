using Brandbank.Api.UploadData;
using Brandbank.Xml.Models.Message;
using Microsoft.Extensions.Logging;
using System;

namespace Brandbank.Api.Clients
{
    public class UploadDataClientLogger : IUploadDataClient
    {
        private readonly ILogger<IUploadDataClient> _logger;
        private readonly IUploadDataClient _uploadClient;
        private int _responseCount;

        public UploadDataClientLogger(ILogger<IUploadDataClient> logger, IUploadDataClient uploadClient)
        {
            _logger = logger;
            _uploadClient = uploadClient;
            _responseCount = 1;
        }

        public byte[] PrepareMessage(Func<MessageType> messageBuilder, string tempDirectory)
        {
            _logger.LogDebug($"Preparing Brandbank message");
            try
            {
                var result = _uploadClient.PrepareMessage(messageBuilder, tempDirectory);
                _logger.LogDebug($"Prepared Brandbank message");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, "Preparing brandbank message failed");
                throw new Exception();
            }
        }

        public UploadResponse UploadMessageToBrandbank(byte[] message)
        {
            _logger.LogDebug($"Uploading message to Brandbank");
            try
            {
                _responseCount = 1;
                var response = _uploadClient.UploadMessageToBrandbank(message);
                return Log(response);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, "Upload to Brandbank failed");
                throw new Exception();
            }
        }

        public UploadResponse GetUploadResponse(Guid receiptId)
        {
            _logger.LogDebug($"Getting upload response for message {receiptId}");
            var response = _uploadClient.GetUploadResponse(receiptId);
            return Log(response);
        }

        private UploadResponse Log(UploadResponse response)
        {
            switch (response.Status)
            {
                case UploadResponse.UploadStatuses.Success:
                    _logger.LogInformation($"Successfully uploaded message {response.ReceiptId} to Brandbank with {response.FilesReceivedCount} file");
                    break;
                case UploadResponse.UploadStatuses.Fail:
                    _logger.LogError($"Failed to uploaded message {response.ReceiptId} to Brandbank");
                    break;
                case UploadResponse.UploadStatuses.Unavailable:
                    _logger.LogCritical($"Brandbank service unavilable");
                    break;
                case UploadResponse.UploadStatuses.Pending:
                    _logger.LogDebug($"Message {response.ReceiptId} pending; Attempt {_responseCount++}");
                    break;
                default:
                    break;
            }              

            foreach (var msg in response.Messages)
                _logger.LogDebug($"{msg.MessageType} {msg.Code} {msg.Text}");

            return response;
        }
    }
}
