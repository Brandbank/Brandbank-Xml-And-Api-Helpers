﻿using Brandbank.Api.Clients;
using Brandbank.Api.ReportData;
using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Coverage;
using Microsoft.Extensions.Logging;
using System;
using System.ServiceModel;
using System.Xml.Schema;

namespace Brandbank.Api
{
    public class CoverageApi
    {
        private readonly Guid _authGuid;
        private readonly string _endpointAddress;
        private readonly string _schema;
        private readonly string _schemaNamespace;
        private readonly ILogger<ICoverageClient> _logger;
        private readonly ValidationEventHandler _validationEventHandler;

        public CoverageApi(Guid authGuid, string endpointAddress, string schema, string schemaNamespace, ILogger<ICoverageClient> logger, ValidationEventHandler validationEventHandler)
        {
            _authGuid = authGuid;
            _endpointAddress = endpointAddress;
            _schema = schema;
            _schemaNamespace = schemaNamespace;
            _logger = logger;
            _validationEventHandler = validationEventHandler;
        }

        public void UploadCoverage(ReportType coverage)
        {
            var client = new DataReportSoapClient(BrandbankHttpsBinding("Data ReportSoap"), BrandbankEndpointAddress(_endpointAddress));
            using (var coverageClient = new CoverageClientLogger(_logger, new CoverageClient(_authGuid, client)))
                UploadCompressedData(coverage, coverageClient.UploadCompressedCoverage);
        }

        private int UploadCompressedData(
            ReportType coverage,
            Func<byte[], int> uploader)
        {
            return coverage
                .ConvertToXml()
                .ValidateXml(_schema, _schemaNamespace, _validationEventHandler)
                .ConvertXmlToStream()
                .CompressMemoryStream("BrandbankCoverage.xml")
                .Then(uploader);
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
