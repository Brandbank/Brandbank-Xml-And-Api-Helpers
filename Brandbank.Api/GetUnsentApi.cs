using Brandbank.Api.Clients;
using Brandbank.Api.ExtractData;
using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using Microsoft.Extensions.Logging;
using System;
using System.ServiceModel;
using System.Xml;
using System.Xml.Schema;

namespace Brandbank.Api
{
    public class GetUnsentApi
    {
        private readonly Guid _authGuid;
        private readonly string _endpointAddress;
        private readonly string _schema;
        private readonly string _schemaNamespace;
        private readonly ValidationEventHandler _validationEventHandler;

        public GetUnsentApi(Guid authGuid, string endpointAddress, string schema, string schemaNamespace, ValidationEventHandler validationEventHandler)
        {
            _authGuid = authGuid;
            _endpointAddress = endpointAddress;
            _schema = schema;
            _schemaNamespace = schemaNamespace;
            _validationEventHandler = validationEventHandler;
        }

        public void GetUnsent(Func<MessageType, IBrandbankMessageSummary> productProcessor, ILogger<IGetUnsentClient> logger)
        {
            var client = new DataExtractSoapClient(BrandbankHttpsBinding("Data ExtractSoap"), BrandbankEndpointAddress(_endpointAddress));
            using (var unsentClient = new GetUnsentClientLogger(logger, new GetUnsentClient(_authGuid, client)))
                GetUnsent(unsentClient, productProcessor);
        }

        public void GetUnsent(IGetUnsentClient unsentClient, Func<MessageType, IBrandbankMessageSummary> productProcessor)
        {
            while (
                unsentClient.GetUnsentProductData()
                    .ValidateXml(_schema, _schemaNamespace, _validationEventHandler)
                    .ConvertTo<MessageType>()
                    .Then(productProcessor)
                    .Then(unsentClient.AcknowledgeMessage)
                    .MessageHadProducts
                );
        }

        private BasicHttpsBinding BrandbankHttpsBinding(string name)
        {
            return new BasicHttpsBinding(BasicHttpsSecurityMode.Transport)
            {
                Name = name,
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferPoolSize = int.MaxValue,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = int.MaxValue,
                    MaxStringContentLength = int.MaxValue
                },
                Security = new BasicHttpsSecurity
                {
                    Mode = BasicHttpsSecurityMode.Transport
                }
            };
        }

        private EndpointAddress BrandbankEndpointAddress(string endpointAddress)
        {
            return new EndpointAddressBuilder { Uri = new Uri(endpointAddress) }.ToEndpointAddress();
        }
    }
}
