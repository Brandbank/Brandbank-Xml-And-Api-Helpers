using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Brandbank.Api.Logging
{
    public class MongoDownloadItem<T> : IDownloadItem<T>
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string BatchId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid ReceiptId { get; set; }
        public string ProductCode { get; set; }
        public bool UploadedToBrandbank { get; set; }
        public bool Acknowledged { get; set; }
        public string MessageText { get; set; }
        public string MessageType { get; set; }
        public T Data { get; set; }
    }
}
