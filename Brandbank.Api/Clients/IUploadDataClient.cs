using Brandbank.Api.UploadData;
using Brandbank.Xml.Models.Message;
using System;
using System.IO;

namespace Brandbank.Api.Clients
{
    public interface IUploadDataClient : IDisposable
    {
        UploadResponse UploadMessage(byte[] message);
        UploadResponse GetResponse(UploadResponse uploadResponse);
    }
}