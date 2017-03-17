using Brandbank.Api.Clients;
using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Xml.Schema;

namespace Brandbank.Api
{
    public class BrandbankApi
    {
        private readonly Guid _authGuid;
        private readonly string _directory;
        private readonly ValidationEventHandler _validationEventHandler;

        public BrandbankApi(Guid authGuid, ValidationEventHandler validationEventHandler, string directory, int historyToKeep = 30)
        {
            _authGuid = authGuid;
            _directory = directory;
            _validationEventHandler = validationEventHandler;
        }

        public void GetUnsent(Func<MessageType, IBrandbankMessageSummary> productProcessor, ILogger<IGetUnsentClient> logger)
        {
            GetUnsent(productProcessor, logger, Path.Combine("Schemas", "BrandbankXML_v6.xsd"), string.Empty);
        }

        public void GetUnsent(Func<MessageType, IBrandbankMessageSummary> productProcessor, ILogger<IGetUnsentClient> logger, string xsdPath, string nameSpace)
        {
            using (var unsentClient = new GetUnsentClientLogger(logger, new GetUnsentClient(_authGuid)))
                while (
                    unsentClient.GetUnsentProductData()
                        .ValidateXml(xsdPath, nameSpace, _validationEventHandler)
                        .ConvertTo<MessageType>()
                        .Then(productProcessor)
                        .Then(unsentClient.AcknowledgeMessage)
                        .MessageHadProducts
                    );
        }

        public void UploadCoverage(Func<Xml.Models.Coverage.ReportType> coverageGetter, ILogger<ICoverageClient> logger)
        {
            UploadCoverage(coverageGetter, logger, Path.Combine("Schemas", "CoverageReportv2a.xsd"), "http://www.brandbank.com/schemas/CoverageFeedback/2005/11");
        }

        public void UploadCoverage(Func<Xml.Models.Coverage.ReportType> coverageGetter, ILogger<ICoverageClient> logger, string xsdPath, string nameSpace)
        {
            using (var coverageClient = new CoverageClientLogger(logger,new CoverageClient(_authGuid)))
            {
                var dir = Path.Combine(_directory, "Coverage", DateTime.Now.ToString("yyyyMMddHHmmssfff")).CreateDirectory();
                uploadCompressedData(dir, xsdPath, nameSpace, coverageGetter, coverageClient.UploadCompressedCoverage);
            }
        }
        private void uploadCompressedData<T>(
            string directory,
            string xsdPath,
            string nameSpace,
            Func<T> productGetter,
            Func<byte[], int> uploader)
            where T : class, new()
        {
            productGetter()
                .ConvertToXml()
                .ValidateXml(xsdPath, nameSpace, _validationEventHandler)
                .SaveToDirectory(directory, "BrandbankData.xml")
                .CompressFolder()
                .Then(uploader);
        }
    }
}
