using Brandbank.Xml.Logging;
using Brandbank.Xml.Models.Message;
using System;
using System.Xml;

namespace Brandbank.Api.Clients
{
    public sealed class GetUnsentClientLogger : IGetUnsentClient
    {
        private readonly ILogger<IGetUnsentClient> _logger;
        private readonly IGetUnsentClient _getUnsentClient;

        public GetUnsentClientLogger(ILogger<IGetUnsentClient> logger, IGetUnsentClient uploadClient)
        {
            _logger = logger;
            _getUnsentClient = uploadClient;
        }

        public IBrandbankMessageSummary AcknowledgeMessage(IBrandbankMessageSummary messageInformation)
        {
            _logger.LogDebug("Acknowledging Brandbank message");
            try
            {
                var result = _getUnsentClient.AcknowledgeMessage(messageInformation);
                _logger.LogDebug("Acknowledged Brandbank message");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Acknowledging brandbank message failed: {e.Message}");
                throw;
            }
        }

        public XmlNode GetUnsentProductData()
        {
            _logger.LogDebug("Getting Unsent Brandbank data");
            try
            {
                var result = _getUnsentClient.GetUnsentProductData();
                _logger.LogDebug("Got Unsent Brandbank data");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting Unsent Brandbank data failed: {e.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing unsent client");
            try
            {
                _getUnsentClient.Dispose();
                _logger.LogDebug("Disposed unsent client");
            }
            catch (Exception e)
            {
                _logger.LogError($"Disposing unsent client failed: {e.Message}");
                throw;
            }
        }
    }
}
