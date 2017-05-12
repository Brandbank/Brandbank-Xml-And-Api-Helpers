using Brandbank.Api.UploadData;
using System;

namespace Brandbank.Api.Clients
{
    public interface IUploadDataClient : IDisposable
    {
        UploadResponse UploadMessage(byte[] message);
        UploadResponse GetResponse(UploadResponse uploadResponse);
    }
}