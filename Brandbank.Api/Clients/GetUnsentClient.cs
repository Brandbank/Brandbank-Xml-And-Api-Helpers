using Brandbank.Api.ExtractData;
using Brandbank.Xml.Models.Message;
using System;
using System.ServiceModel;
using System.Xml;

namespace Brandbank.Api.Clients
{
    public class GetUnsentClient : IGetUnsentClient
    {
        private readonly DataExtractSoapClient _dataExtractSoapClient;
        private readonly ExternalCallerHeader _header;

        public GetUnsentClient(Guid guid, DataExtractSoapClient client)
        {
            _dataExtractSoapClient = client;
            _header = new ExternalCallerHeader
            {
                ExternalCallerId = guid
            };
        }

        public IBrandbankMessageSummary AcknowledgeMessage(IBrandbankMessageSummary messageInformation)
        {
            try
            {
                _dataExtractSoapClient.AcknowledgeMessage(_header, messageInformation.MessageGuid);
                return messageInformation;
            }
            catch (CommunicationException e)
            {
                _dataExtractSoapClient.Abort();
                throw new CommunicationException("CommunicationException", e);
            }
            catch (TimeoutException e)
            {
                _dataExtractSoapClient.Abort();
                throw new TimeoutException("TimeoutException", e);
            }
            catch (Exception e)
            {
                _dataExtractSoapClient.Abort();
                throw new Exception("TimeoutException", e);
            }
        }

        public XmlNode GetUnsentProductData()
        {
            try
            {
                return _dataExtractSoapClient.GetUnsentProductData(_header);
            }
            catch (CommunicationException e)
            {
                _dataExtractSoapClient.Abort();
                throw new CommunicationException("CommunicationException", e);
            }
            catch (TimeoutException e)
            {
                _dataExtractSoapClient.Abort();
                throw new TimeoutException("TimeoutException", e);
            }
            catch (Exception e)
            {
                _dataExtractSoapClient.Abort();
                throw new Exception("Exception", e);
            }
        }

        public void Dispose()
        {
            _dataExtractSoapClient.Close();
        }
    }
}
