using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brandbank.Http.BbHttpClientResponseReader
{
    public interface IBbHttpClientResponseReader
    {
        Task<Stream> GetReadAsStreamAsync(string url);
        Task<Stream> GetReadAsStreamAsync(string url, Dictionary<string, string> headers);
        Task<byte[]> GetReadAsByteAsync(string url);
        Task<byte[]> GetReadAsByteAsync(string url, Dictionary<string, string> headers);
        Task<Stream> PostReadAsStreamAsync(string url, HttpContent httpContent);
        Task<Stream> PostReadAsStreamAsync(string url, FormUrlEncodedContent formUrlEncodedContent);
    }
}