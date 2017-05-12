using System.Collections.Generic;

namespace Brandbank.Xml.Downloader
{
    public interface IDownloader<out T>
    {
        IEnumerable<T> DownloadData();
    }
}
