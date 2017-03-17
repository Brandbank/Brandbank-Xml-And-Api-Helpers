using Brandbank.Api.UploadData;
using Brandbank.Xml.Models.Message;
using System;
using System.IO;

namespace Brandbank.Api.Clients
{
    public interface IUploadDataClient
    {
        byte[] PrepareMessage(Func<MessageType> messageBuilder, string tempDirectory);
        UploadResponse UploadMessageToBrandbank(byte[] message);
        UploadResponse GetUploadResponse(Guid receiptId);
    }
}