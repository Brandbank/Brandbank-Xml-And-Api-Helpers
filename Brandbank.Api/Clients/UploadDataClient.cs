using Brandbank.Api.UploadData;
using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using System;
using System.IO;
using System.Xml.Schema;

namespace Brandbank.Api.Clients
{
    public class UploadDataClient : IUploadDataClient
    {
        private readonly UploadClient _uploadClient;
        private readonly UserCredentialHeader _header;
        private readonly string _xsdSchema;
        private readonly string _nameSpace;
        private readonly ValidationEventHandler _validationEventHandler;

        public UploadDataClient(Guid guid, string xsdSchema, string nameSpace, ValidationEventHandler validationEventHandler)
        {
            if (guid == null)
                throw new NullReferenceException("Guid cannot be null");

            _uploadClient = new UploadClient();
            _header = new UserCredentialHeader
            {
                UserCredential = guid
            };
            _xsdSchema = xsdSchema;
            _nameSpace = nameSpace;
            _validationEventHandler = validationEventHandler;
        }

        public byte[] PrepareMessage(Func<MessageType> messageBuilder, string uploadDirectory)
        {
            return messageBuilder()
                .ConvertToXml()
                .ValidateXml(_xsdSchema, _nameSpace, _validationEventHandler)
                .SaveToDirectory(uploadDirectory, "BrandbankMessage.xml")
                .CompressFolder();
        }

        public UploadResponse UploadMessageToBrandbank(byte[] message)
        {
            using (var data = new MemoryStream(message))
               return _uploadClient.UploadZip(_header, data);
        } 

        public UploadResponse GetUploadResponse(Guid receiptId) => _uploadClient.GetUploadResponse(_header, receiptId);
    }
}
