using Brandbank.Xml.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brandbank.Http.BbHttpClientResponseReader
{
    public class BbHttpClientResponseReaderLogger : IBbHttpClientResponseReader
    {
        private readonly ILogger<IBbHttpClientResponseReader> _logger;
        private readonly IBbHttpClientResponseReader _reader;

        public BbHttpClientResponseReaderLogger(ILogger<IBbHttpClientResponseReader> logger, IBbHttpClientResponseReader reader)
        {
            _logger = logger;
            _reader = reader;
        }

        public async Task<Stream> GetReadAsStreamAsync(string url)
        {
            _logger.LogDebug($"Reading data from Get to {url}");
            var response = await _reader.GetReadAsStreamAsync(url);
            _logger.LogDebug(response == null
                                 ? $"No data recieved for get to {url}"
                                 : $"Read response from {url}");
            return response;
        }

        public async Task<Stream> GetReadAsStreamAsync(string url, Dictionary<string, string> headers)
        {
            _logger.LogDebug($"Reading data from Get to {url} with custom headers {string.Join(";", headers.Select(x => x.Key + "=" + x.Value))}");
            var response = await _reader.GetReadAsStreamAsync(url, headers);
            _logger.LogDebug(response == null
                                 ? $"No data recieved for get to {url}"
                                 : $"Read response from {url}");
            return response;
        }

        public async Task<byte[]> GetReadAsByteAsync(string url)
        {
            _logger.LogDebug($"Reading data from Get to {url}");
            var response = await _reader.GetReadAsByteAsync(url);
            _logger.LogDebug(response == null
                                 ? $"No data recieved for get to {url}"
                                 : $"Read response from {url}");
            return response;
        }

        public async Task<byte[]> GetReadAsByteAsync(string url, Dictionary<string, string> headers)
        {
            _logger.LogDebug($"Reading data from Get to {url} with custom headers {string.Join(";", headers.Select(x => x.Key + "=" + x.Value))}");
            var response = await _reader.GetReadAsByteAsync(url, headers);
            _logger.LogDebug(response == null
                                 ? $"No data recieved for get to {url}"
                                 : $"Read response from {url}");
            return response;
        }

        public async Task<Stream> PostReadAsStreamAsync(string url, HttpContent httpContent)
        {
            _logger.LogDebug($"Reading data from Post to {url}");
            var response = await _reader.PostReadAsStreamAsync(url, httpContent);
            _logger.LogDebug(response == null
                                 ? $"No data recieved from post to {url}"
                                 : $"Read response from {url}");
            return response;
        }

        public async Task<Stream> PostReadAsStreamAsync(string url, FormUrlEncodedContent formUrlEncodedContent)
        {
            _logger.LogDebug($"Reading data from Post to {url}");
            var response = await _reader.PostReadAsStreamAsync(url, formUrlEncodedContent);
            _logger.LogDebug(response == null
                                 ? $"No data recieved from post to {url}"
                                 : $"Read response from {url}");
            return response;
        }
    }
}
