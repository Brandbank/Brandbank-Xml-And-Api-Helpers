using System.IO;

namespace Brandbank.Xml.ImageDownloader
{
    public interface IBrandbankImageDownloader
    {
        Stream DownloadToStream(string url);
        byte[] DownloadToByteArray(string url);
    }
}
