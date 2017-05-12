using Microsoft.Extensions.Logging;
using System;

namespace Brandbank.Api.Clients
{
    public class FeedbackClientLogger : IFeedbackClient
    {
        private readonly ILogger<IFeedbackClient> _logger;
        private readonly IFeedbackClient _feedbackClient;

        public FeedbackClientLogger(ILogger<IFeedbackClient> logger, IFeedbackClient feedbackClient)
        {
            _logger = logger;
            _feedbackClient = feedbackClient;
        }

        public int UploadCompressedFeedback(byte[] compressedFeedback)
        {
            _logger.LogDebug("Uploading compressed feedback to Brandbank");
            try
            {
                var response = _feedbackClient.UploadCompressedFeedback(compressedFeedback);
                _logger.LogDebug(response == 0
                                     ? "Uploaded compressed feedback to Brandbank"
                                     : $"Upload compressed feedback to Brandbank failed, response code {response}");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, "Upload compressed feedback to Brandbank failed");
                throw new Exception("Upload compressed feedback to Brandbank failed", e);
            }
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing feedback client");
            try
            {
                _feedbackClient.Dispose();
                _logger.LogDebug("Disposed feedback client");
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, "Disposing feedback client failed");
                throw new Exception("Disposing feedback client failed", e);
            }
        }
    }
}
