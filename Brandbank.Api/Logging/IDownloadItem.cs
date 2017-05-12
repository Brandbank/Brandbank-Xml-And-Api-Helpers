using System;

namespace Brandbank.Api.Logging
{
    public interface IDownloadItem<T>
    {
        string Id { get; set; }
        string BatchId { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        Guid ReceiptId { get; set; }
        string ProductCode { get; set; }
        bool Acknowledged { get; set; }
        bool UploadedToBrandbank { get; set; }
        string MessageText { get; set; }
        string MessageType { get; set; }
        T Data { get; set; }
    }
}
