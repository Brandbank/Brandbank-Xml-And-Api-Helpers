using Brandbank.Xml.Logging;
using System;

namespace Brandbank.Api.Clients
{
    public sealed class CoverageClientLogger : ICoverageClient
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
            _logger.LogDebug("Uploading compressed coverage to Brandbank");
            try
            {
                var response = _coverageClient.UploadCompressedCoverage(compressedCoverage);
                _logger.LogDebug(response == 0
                                     ? "Uploaded compressed coverage to Brandbank"
                                     : $"Upload compressed coverage to Brandbank failed, response code {response}");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"Upload to Brandbank failed: {e}");
                throw;
            }
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing coverage client");
            try
            {
                _coverageClient.Dispose();
                _logger.LogDebug("Disposed coverage client");
            }
            catch (Exception e)
            {
                _logger.LogError($"Disposing coverage client failed: {e}");
                throw;
            }
        }
    }
}
