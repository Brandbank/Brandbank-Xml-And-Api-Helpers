using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Brandbank.Xml.Downloader
{
    public class DownloaderLogger<T> : IDownloader<T>
    {
        ILogger<IDownloader<T>> _logger;
        IDownloader<T> _downloader;

        public DownloaderLogger(ILogger<IDownloader<T>> logger, IDownloader<T> downloader)
        {
            _logger = logger;
            _downloader = downloader;
        }

        public IEnumerable<T> DownloadData()
        {
            _logger.LogDebug($"Downloading data");
            try
            {
                var item = _downloader.DownloadData();
                if (item != null)
                    _logger.LogDebug($"Downloaded data");
                else
                    _logger.LogDebug($"Failed to download data from");
                return item;
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Failed to download data");
                throw new Exception("Downloader");
            }
        }
    }
}
