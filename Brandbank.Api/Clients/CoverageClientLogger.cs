using Microsoft.Extensions.Logging;
using System;

namespace Brandbank.Api.Clients
{
    public class CoverageClientLogger : ICoverageClient
    {
        private readonly ILogger<ICoverageClient> _logger;
        private readonly ICoverageClient _coverageClient;

        public CoverageClientLogger(ILogger<ICoverageClient> logger, ICoverageClient coverageClient)
        {
            _logger = logger;
            _coverageClient = coverageClient;
        }

        public int UploadCompressedCoverage(byte[] compressedCoverage)
        {
            _logger.LogDebug($"Uploading compressed coverage to Brandbank");
            try
            {
                var response = _coverageClient.UploadCompressedCoverage(compressedCoverage);
                if (response == 0)
                    _logger.LogDebug($"Uploaded compressed coverage to Brandbank");
                else
                    _logger.LogDebug($"Upload compressed coverage to Brandbank failed, response code {response}");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, "Upload to Brandbank failed");
                throw new Exception("Problem uploading compressed coverage to Brandbank", e);
            }
        }

        public void Dispose()
        {
            _logger.LogDebug($"Disposing coverage client");
            try
            {
                _coverageClient.Dispose();
                _logger.LogDebug($"Disposed coverage client");
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Disposing coverage client failed: {e.Message}");
                throw new Exception("Disposing coverage client failed", e);
            }
        }
    }
}
