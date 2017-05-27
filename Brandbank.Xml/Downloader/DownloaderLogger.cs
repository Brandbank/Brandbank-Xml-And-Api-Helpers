using Brandbank.Xml.Logger;
using System;
using System.Collections.Generic;

namespace Brandbank.Xml.Downloader
{
    public class DownloaderLogger<T> : IDownloader<T>
    {
        readonly ILogger<IDownloader<T>> _logger;
        readonly IDownloader<T> _downloader;

        public DownloaderLogger(ILogger<IDownloader<T>> logger, IDownloader<T> downloader)
        {
            _logger = logger;
            _downloader = downloader;
        }

        public IEnumerable<T> DownloadData()
        {
            _logger.LogDebug("Downloading data");
            try
            {
                var item = _downloader.DownloadData();
                _logger.LogDebug(item != null
                                     ? "Downloaded data"
                                     : "Failed to download data from");
                return item;
            }
            catch (Exception)
            {
                _logger.LogError("Failed to download data");
                throw new Exception("Downloader");
            }
        }
    }
}
