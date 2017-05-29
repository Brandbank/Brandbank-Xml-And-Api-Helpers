using Brandbank.Http.BbHttpClient;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brandbank.Http.BbHttpClientResponseReader
{
    public class BbHttpClientResponseReader : IBbHttpClientResponseReader
    {
        private readonly IBbHttpClient _client;

        public BbHttpClientResponseReader(IBbHttpClient client)
        {
            _client = client;
        }

        public async Task<Stream> GetReadAsStreamAsync(string url)
        {
            return await GetReadAsStreamAsync(url, new Dictionary<string, string>());
        }

        public async Task<Stream> GetReadAsStreamAsync(string url, Dictionary<string, string> headers)
        {
            var response = await _client.GetAsync(url, headers);
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<byte[]> GetReadAsByteAsync(string url)
        {
            return await GetReadAsByteAsync(url, new Dictionary<string, string>());
        }

        public async Task<byte[]> GetReadAsByteAsync(string url, Dictionary<string, string> headers)
        {
            var response = await _client.GetAsync(url, headers);
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<Stream> PostReadAsStreamAsync(string url, HttpContent httpContent)
        {
            var response = await _client.PostAsync(url, httpContent);
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> PostReadAsStreamAsync(string url, FormUrlEncodedContent formUrlEncodedContent)
        {
            var response = await _client.PostAsync(url, formUrlEncodedContent);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
