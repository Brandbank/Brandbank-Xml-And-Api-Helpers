using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brandbank.Xml.BbHttpClient
{
    public class BbHttpClientLogger : IBbHttpClient
    {
        private readonly ILogger _logger;
        private readonly IBbHttpClient _httpClient;

        public BbHttpClientLogger(ILogger logger, IBbHttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            _logger.LogDebug($"Getting data from {url}");
            var response = await _httpClient.GetAsync(url);
            Log(response, url);
            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers)
        {
            _logger.LogDebug($"Getting data from {url} with custom headers {string.Join(";", headers.Select(x => x.Key + "=" + x.Value))}");
            var response = await _httpClient.GetAsync(url, headers);
            Log(response, url);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent httpContent)
        {
            _logger.LogDebug($"Posting data to {url}");
            var response = await _httpClient.PostAsync(url, httpContent);
            Log(response, url);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, FormUrlEncodedContent formUrlEncodedContent)
        {
            _logger.LogDebug($"Posting data to {url}");
            var response = await _httpClient.PostAsync(url, formUrlEncodedContent);
            Log(response, url);
            return response;
        }

        private void Log(HttpResponseMessage response, string url)
        {
            if (response.IsSuccessStatusCode)
                _logger.LogDebug($"Got data from {url}: Status Code: {response.StatusCode}");
            else
                _logger.LogError($"Getting data form {url} failed: Status Code: {response.StatusCode}: Reason: {response.ReasonPhrase}");
        }
    }
}
