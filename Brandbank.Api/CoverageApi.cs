using Brandbank.Api.Clients;
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
        private readonly ValidationEventHandler _validationEventHandler;
        private readonly DataReportSoapClient _dataReportSoapClient;

        public CoverageApi(Guid authGuid, string endpointAddress, ValidationEventHandler validationEventHandler)
        {
            _dataReportSoapClient = new DataReportSoapClient(BrandbankHttpsBinding("Data ReportSoap"), BrandbankEndpointAddress(endpointAddress));
            _authGuid = authGuid;
            _validationEventHandler = validationEventHandler;
        }

        public void UploadCoverage(ReportType coverage, ILogger<ICoverageClient> logger, string xsd, string nameSpace)
        {
            using (var coverageClient = new CoverageClientLogger(logger, new CoverageClient(_authGuid, _dataReportSoapClient)))
            {
                UploadCompressedData(xsd, nameSpace, coverage, coverageClient.UploadCompressedCoverage);
            }
        }

        private void UploadCompressedData(
            string xsd,
            string nameSpace,
            ReportType coverage,
            Func<byte[], int> uploader)
        {
            coverage.ConvertToXml()
                .ValidateXml(xsd, nameSpace, _validationEventHandler)
                .ConvertXmlToStream()
                .CompressMemoryStream("Brandbank.Xml")
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

        private  EndpointAddress BrandbankEndpointAddress(string endpointAddress)
        {
            return new EndpointAddressBuilder { Uri = new Uri(endpointAddress) }.ToEndpointAddress();
        }

    }
}
