using Brandbank.Api.Clients;
using Brandbank.Api.UploadData;
using System;
using System.Threading;

namespace Brandbank.Api.Extensions
{
    public static class UploadDataClientExtensions
    {
        public static UploadResponse GetResponse(this IUploadDataClient uploader, UploadResponse uploadResponse, int wait = 2)
        {
            while (uploadResponse.Status == UploadResponse.UploadStatuses.Pending)
            {
                if (uploadResponse.Status == UploadResponse.UploadStatuses.Pending)
                    Thread.Sleep(TimeSpan.FromMinutes(wait));
                uploadResponse = uploader.GetUploadResponse(uploadResponse.ReceiptId);
            }
            return uploadResponse;
        }
    }
}
