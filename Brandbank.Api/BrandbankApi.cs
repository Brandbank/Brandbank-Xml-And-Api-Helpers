using Brandbank.Api.Clients;
using Brandbank.Api.ExtractData;
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

        public BrandbankApi(Guid authGuid, string directory) : this(authGuid, directory, CreateValidationEventHandler())
        {
        }

        public BrandbankApi(Guid authGuid, string directory, ValidationEventHandler validationEventHandler)
        {
            _authGuid = authGuid;
            _directory = directory;
            _validationEventHandler = validationEventHandler;
        }

        public BrandbankApi(DataExtractSoapClient client, string directory, ValidationEventHandler validationEventHandler)
        {
            _authGuid = new Guid();
            _directory = directory;
            _validationEventHandler = validationEventHandler;
        }

        public void GetUnsent(Func<MessageType, IBrandbankMessageSummary> productProcessor)
        {
            GetUnsent(productProcessor, CreateLogger<IGetUnsentClient>);
        }

        public void GetUnsent(Func<MessageType, IBrandbankMessageSummary> productProcessor, Func<ILogger<IGetUnsentClient>> logger)
        {
            GetUnsent(productProcessor, logger, Path.Combine("Schemas", "BrandbankXML_v6.xsd"), string.Empty);
        }

        public void GetUnsent(Func<MessageType, IBrandbankMessageSummary> productProcessor, Func<ILogger<IGetUnsentClient>> logger, string xsdPath, string nameSpace)
        {
            using (var unsentClient = new GetUnsentClientLogger(logger(), new GetUnsentClient(_authGuid)))
                GetUnsent(unsentClient, productProcessor, xsdPath, nameSpace);
        }

        public void GetUnsent(Func<MessageType, IBrandbankMessageSummary> productProcessor, Func<ILogger<IGetUnsentClient>> logger, DataExtractSoapClient client, string xsdPath, string nameSpace)
        {
            using (var unsentClient = new GetUnsentClientLogger(logger(), new GetUnsentClient(_authGuid, client)))
                GetUnsent(unsentClient, productProcessor, xsdPath, nameSpace);
        }

        public void GetUnsent(IGetUnsentClient unsentClient, Func<MessageType, IBrandbankMessageSummary> productProcessor, string xsdPath, string nameSpace)
        {
            while (
                unsentClient.GetUnsentProductData()
                    //.ValidateXml(xsdPath, nameSpace, _validationEventHandler)
                    .ConvertTo<MessageType>()
                    .Then(productProcessor)
                    .Then(unsentClient.AcknowledgeMessage)
                    .MessageHadProducts
                );
        }

        private static ILogger<T> CreateLogger<T>()
        {
            var loggerFactory = new LoggerFactory();
            return loggerFactory.CreateLogger<T>();
        }

        private static ValidationEventHandler CreateValidationEventHandler()
        {
            return (o, e) =>
            {
                var message = $"{e.Severity}: {e.Message} [Line {e.Exception.LineNumber}, Pos {e.Exception.LinePosition}]";
                Console.WriteLine(message);
            };
        }
    }
}
