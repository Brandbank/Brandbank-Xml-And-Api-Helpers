﻿using Brandbank.Api.UploadData;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;

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

        public UploadResponse UploadMessage(byte[] message)
        {
            _logger.LogDebug("Uploading message to Brandbank");
            try
            {
                _responseCount = 1;
                var response = _uploadClient.UploadMessage(message);
                return Log(response);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, "Upload to Brandbank failed");
                throw new Exception();
            }
        }

        public UploadResponse GetResponse(UploadResponse uploadResponse)
        {
            _logger.LogDebug($"Getting upload response for message {uploadResponse.ReceiptId}");
            var response = _uploadClient.GetResponse(uploadResponse);
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
                    _logger.LogCritical("Brandbank service unavilable");
                    break;
                case UploadResponse.UploadStatuses.Pending:
                    _logger.LogDebug($"Message {response.ReceiptId} pending; Attempt {_responseCount++}");
                    break;
            }

            foreach (var msg in response.Messages)
            {
                var logMessage = Regex.Replace($"{msg.MessageType} {msg.Code} {msg.Text}", @"\r\n?|\n|\t", " ");
                switch (msg.MessageType)
                {
                    case Message.MessageTypes.Error:
                        _logger.LogError(logMessage);
                        break;
                    case Message.MessageTypes.Warning:
                        _logger.LogWarning(logMessage);
                        break;
                    case Message.MessageTypes.Information:
                        _logger.LogInformation(logMessage);
                        break;
                }
            }
            return response;
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing upload client");
            try
            {
                _uploadClient.Dispose();
                _logger.LogDebug("Disposed upload client");
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Disposing upload client failed: {e.Message}");
                throw;
            }
        }
    }
}
