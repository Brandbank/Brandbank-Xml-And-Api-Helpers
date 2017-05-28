using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brandbank.Xml.BbHttpClient
{
    public interface IBbHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent httpContent);
        Task<HttpResponseMessage> PostAsync(string url, FormUrlEncodedContent formUrlEncodedContent);
    }
}