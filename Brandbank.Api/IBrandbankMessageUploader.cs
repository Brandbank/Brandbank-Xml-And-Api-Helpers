using Brandbank.Api.UploadData;
using Brandbank.Xml.Models.Message;

namespace Brandbank.Api
{
    public interface IBrandbankMessageUploader
    {
        UploadResponse UploadData(MessageType message);
    }
}