using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brandbank.Xml.BbHttpClient
{
    public class BbHttpClient : IBbHttpClient
    {
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await GetAsync(url, new Dictionary<string, string>());
        }

        public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers)
        {
            using (var httpClient = new HttpClient())
            {
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                return await httpClient.GetAsync(url);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent httpContent)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.PostAsync(url, httpContent);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string url, FormUrlEncodedContent formUrlEncodedContent)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.PostAsync(url, formUrlEncodedContent);
            }
        }
    }
}
