using Brandbank.Api.Clients;
using Brandbank.Api.UploadData;
using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Xml.Schema;

namespace Brandbank.Api
{
    public class UploadDataApi
    {
        private readonly Guid _authGuid;
        private readonly ValidationEventHandler _validationEventHandler;
        private readonly ILogger<IUploadDataClient> _logger;

        public UploadDataApi(Guid authGuid, ValidationEventHandler validator, ILogger<IUploadDataClient> logger)
        {
            _authGuid = authGuid;
            _logger = logger ?? throw new NullReferenceException("validator");
            _validationEventHandler = validator ?? throw new NullReferenceException("logger");
        }

        public UploadResponse UploadData(MessageType message, string uploadDirectory)
        {
            var xsdSchema = Path.Combine("Schemas", "BrandbankXML_v6.xsd");
            var nameSpace = string.Empty;
            using (var uploadDataClient = new UploadDataClientLogger(_logger, new UploadDataClient(_authGuid)))
            {
                return message
                    .ConvertToXml()
                    .ValidateXml(xsdSchema, nameSpace, _validationEventHandler)
                    .SaveToDirectory(uploadDirectory, "BrandbankMessage.xml")
                    .CompressFolder()
                    .Then(uploadDataClient.UploadMessage)
                    .Then(uploadDataClient.GetResponse);
            }
        }
    }
}
