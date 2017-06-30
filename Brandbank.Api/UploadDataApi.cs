using Brandbank.Api.Clients;
using Brandbank.Api.UploadData;
using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Logging;
using System;
using System.ServiceModel;
using System.Xml.Schema;
using System.Net;

namespace Brandbank.Api
{
    public class UploadDataApi : IBrandbankMessageUploader
    {
        private readonly Guid _authGuid;
        private readonly string _endpointAddress;
        private readonly string _schema;
        private readonly string _schemaNamespace;
        private readonly ILogger<IUploadDataClient> _logger;
        private readonly ValidationEventHandler _validationEventHandler;
        private readonly string _proxy;
        private readonly string _username;
        private readonly string _password;

        public UploadDataApi(
            Guid authGuid, 
            string endpointAddress, 
            string schema, 
            string schemaNamespace, 
            ILogger<IUploadDataClient> logger, 
            ValidationEventHandler validationEventHandler,
            string proxy, 
            string username, 
            string password)
        {
            _authGuid = authGuid;
            _endpointAddress = endpointAddress;
            _schema = schema;
            _schemaNamespace = schemaNamespace;
            _logger = logger;
            _validationEventHandler = validationEventHandler;
            _proxy = proxy;
            _username = username;
            _password = password;
        }

        public UploadDataApi(
            Guid authGuid, 
            string endpointAddress, 
            string schema, 
            string schemaNamespace, 
            ILogger<IUploadDataClient> logger, 
            ValidationEventHandler validationEventHandler)
            : this(
                  authGuid,
                  endpointAddress,
                  schema,
                  schemaNamespace,
                  logger,
                  validationEventHandler,
                  null,
                  null,
                  null)
        {
        }

        public UploadResponse UploadData(MessageType message)
        {
            var uploadClient = new UploadClient(BrandbankHttpsBinding("BasicHttpBinding_IUpload"), BrandbankEndpointAddress(_endpointAddress));

            using (var uploadDataClient = new UploadDataClientLogger(_logger, new UploadDataClient(_authGuid, uploadClient)))
            {
                var orginalProxy = WebRequest.DefaultWebProxy;

                try
                {
                    if (_proxy != null)
                    {
                        WebRequest.DefaultWebProxy = new WebProxy(_proxy)
                        {
                            Credentials = new NetworkCredential(_username, _password)
                        };
                    }

                    return UploadData(message, uploadDataClient);
                }
                finally
                {
                    WebRequest.DefaultWebProxy = orginalProxy;
                }
            }
        }

        private UploadResponse UploadData(MessageType message, IUploadDataClient uploadDataClient)
        {
            return message
                .ConvertToXml()
                .ValidateXml(_schema, _schemaNamespace, _validationEventHandler)
                .ConvertXmlToStream()
                .CompressMemoryStream("BrandbankCoverage.xml")
                .Then(uploadDataClient.UploadMessage)
                .Then(uploadDataClient.GetResponse);
        }

        private BasicHttpsBinding BrandbankHttpsBinding(string name)
        {
            return new BasicHttpsBinding(BasicHttpsSecurityMode.Transport)
            {
                Name = name,
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
