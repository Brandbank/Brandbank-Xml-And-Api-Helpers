using Brandbank.Api.UploadData;
using System;
using System.IO;
using System.ServiceModel;
using System.Threading;

namespace Brandbank.Api.Clients
{
    public sealed class UploadDataClient : IUploadDataClient
    {
        private readonly UploadClient _uploadClient;
        private readonly UserCredentialHeader _header;
        private readonly int _wait;

        public UploadDataClient(Guid guid, UploadClient uploadClient, int wait = 20)
        {
            _uploadClient = uploadClient;
            _header = new UserCredentialHeader
            {
                UserCredential = guid
            };
            _wait = wait;
        }

        public UploadResponse UploadMessage(byte[] message)
        {
            using (var data = new MemoryStream(message))
               return _uploadClient.UploadZip(_header, data);
        } 

        public UploadResponse GetResponse(UploadResponse uploadResponse)
        {
            while (uploadResponse.Status == UploadResponse.UploadStatuses.Pending)
            {
                if (uploadResponse.Status == UploadResponse.UploadStatuses.Pending)
                    Thread.Sleep(TimeSpan.FromSeconds(_wait));
                uploadResponse = _uploadClient.GetUploadResponse(_header, uploadResponse.ReceiptId);
            }
            return uploadResponse;
        }

        public void Dispose()
        {
            _uploadClient.Close();
        }
    }
}
