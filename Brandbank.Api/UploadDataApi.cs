﻿using Brandbank.Api.Clients;
using Brandbank.Api.UploadData;
using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using Microsoft.Extensions.Logging;
using System;
using System.ServiceModel;
using System.Xml.Schema;

namespace Brandbank.Api
{
    public class UploadDataApi
    {
        private readonly Guid _authGuid;
        private readonly string _endpointAddress;
        private readonly string _schema;
        private readonly string _schemaNamespace;
        private readonly ValidationEventHandler _validationEventHandler;

        public UploadDataApi(Guid authGuid, string endpointAddress, string schema, string schemaNamespace, ValidationEventHandler validationEventHandler)
        {
            _authGuid = authGuid;
            _endpointAddress = endpointAddress;
            _schema = schema;
            _schemaNamespace = schemaNamespace;
            _validationEventHandler = validationEventHandler;
        }

        public UploadResponse UploadData(MessageType message, ILogger<IUploadDataClient> logger, string uploadDirectory)
        {
            var uploadClient = new UploadClient(BrandbankHttpsBinding("BasicHttpBinding_IUpload"), BrandbankEndpointAddress(_endpointAddress));
            using (var uploadDataClient = new UploadDataClientLogger(logger, new UploadDataClient(_authGuid, uploadClient)))
            {
                return message
                    .ConvertToXml()
                    .ValidateXml(_schema, _schemaNamespace, _validationEventHandler)
                    .SaveToDirectory(uploadDirectory, "BrandbankMessage.xml")
                    .CompressFolder()
                    .Then(uploadDataClient.UploadMessage)
                    .Then(uploadDataClient.GetResponse);
            }
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
