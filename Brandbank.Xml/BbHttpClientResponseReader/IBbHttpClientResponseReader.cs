using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brandbank.Xml.BbHttpClientResponseReader
{
    public interface IBbHttpClientResponseReader
    {
        Task<Stream> GetReadAsStreamAsync(string url);
        Task<Stream> GetReadAsStreamAsync(string url, Dictionary<string, string> headers);
        Task<Stream> PostReadAsStreamAsync(string url, HttpContent httpContent);
        Task<Stream> PostReadAsStreamAsync(string url, FormUrlEncodedContent formUrlEncodedContent);
    }
}