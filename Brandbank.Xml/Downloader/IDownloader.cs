using System.Collections.Generic;
using System.IO;

namespace Brandbank.Xml.Downloader
{
    public interface IDownloader<T>
    {
        IEnumerable<T> DownloadData();
    }
}
