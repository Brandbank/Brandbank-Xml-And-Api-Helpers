using Brandbank.Xml.Logging;
using System;
using System.IO;

namespace Brandbank.Xml.ImageDownloader
{
    public class BrandbankImageDownloaderLogger : IBrandbankImageDownloader
    {
        readonly ILogger<IBrandbankImageDownloader> _logger;
        readonly IBrandbankImageDownloader _downloader;

        public BrandbankImageDownloaderLogger(ILogger<IBrandbankImageDownloader> logger, IBrandbankImageDownloader downloader)
        {
            _logger = logger;
            _downloader = downloader;
        }

        public Stream DownloadToStream(string url)
        {
            return Log(_downloader.DownloadToStream, url, "Stream");
        }

        public byte[] DownloadToByteArray(string url)
        {
            return Log(_downloader.DownloadToByteArray, url, "Byte Array");
        }

        private T Log<T>(Func<string, T> fn, string url, string type)
        {
            _logger.LogDebug($"Downloading image to {type} from {url}");
            try
            {
                var item = fn(url);
                _logger.LogDebug(item != null
                                     ? $"Downloaded image as {type} from {url}"
                                     : $"Failed to download data {type} as tream from {url}");
                return item;
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to download {type} as stream from {url}");
                throw;
            }
        }

        
    }
}
