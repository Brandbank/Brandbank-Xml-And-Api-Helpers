using System.IO;
using System.Net.Http;

namespace Brandbank.Xml.ImageDownloader
{
    public class ImageDownloader : IBrandbankImageDownloader
    {
        public Stream DownloadToStream(string url)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStreamAsync().Result;
        }

        public byte[] DownloadToByteArray(string url)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(url).Result;
            return response.Content.ReadAsByteArrayAsync().Result;
        }
    }
}
