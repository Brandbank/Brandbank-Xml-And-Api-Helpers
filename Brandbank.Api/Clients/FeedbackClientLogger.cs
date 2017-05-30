using Brandbank.Xml.Logging;
using System;

namespace Brandbank.Api.Clients
{
    public sealed class FeedbackClientLogger : IFeedbackClient
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
                _logger.LogError($"Upload compressed feedback to Brandbank failed: {e}");
                throw;
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
                _logger.LogError($"Disposing feedback client failed: {e}");
                throw;
            }
        }
    }
}
