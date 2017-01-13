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

        public UploadDataClient(Guid guid, string endpointAddress)
        {
            if (guid == null)
                throw new NullReferenceException("Guid cannot be null");

            _uploadClient = new UploadClient();
            _header = new UserCredentialHeader
            {
                UserCredential = guid
            };
        }

        public byte[] PrepareMessage(Func<MessageType> messageBuilder, string uploadDirectory)
        {
            var newDirectory = Path.Combine(uploadDirectory, DateTime.Now.ToString("yyyyMMddHHmmss"));

            return messageBuilder()
                .ConvertToXml()
                .ValidateXml("", validationEventHandler)
                .CreateDirectory(newDirectory)
                .SaveToDirectory(newDirectory, "BrandbankMessage.xml")
                .CompressFolder();
        }

        public UploadResponse UploadMessageToBrandbank(Stream message) => _uploadClient.UploadZip(_header, message);

        public UploadResponse GetUploadResponse(Guid receiptId) => _uploadClient.GetUploadResponse(_header, receiptId);

        private ValidationEventHandler validationEventHandler()
        {
            return (o, e) => { };
        }
    }
}
