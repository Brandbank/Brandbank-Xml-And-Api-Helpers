using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brandbank.Xml.BbHttpClientResponseReader
{
    public class BbHttpClientResponseReaderLogger : IBbHttpClientResponseReader
    {
        private readonly ILogger _logger;
        private readonly IBbHttpClientResponseReader _reader;

        public BbHttpClientResponseReaderLogger(ILogger logger, IBbHttpClientResponseReader reader)
        {
            _logger = logger;
            _reader = reader;
        }

        public async Task<Stream> GetReadAsStreamAsync(string url)
        {
            _logger.LogDebug($"Reading data from Get to {url}");
            var response = await _reader.GetReadAsStreamAsync(url);
            if (response == null)
                _logger.LogDebug($"No data recieved for get to {url}");
            else
                _logger.LogDebug($"Read response from {url}");
            return response;
        }

        public async Task<Stream> GetReadAsStreamAsync(string url, Dictionary<string, string> headers)
        {
            _logger.LogDebug($"Reading data from Get to {url} with custom headers {string.Join(";", headers.Select(x => x.Key + "=" + x.Value))}");
            var response = await _reader.GetReadAsStreamAsync(url, headers);
            if (response == null)
                _logger.LogDebug($"No data recieved for get to {url}");
            else
                _logger.LogDebug($"Read response from {url}");
            return response;
        }

        public async Task<Stream> PostReadAsStreamAsync(string url, HttpContent httpContent)
        {
            _logger.LogDebug($"Reading data from Post to {url}");
            var response = await _reader.PostReadAsStreamAsync(url, httpContent);
            if (response == null)
                _logger.LogDebug($"No data recieved from post to {url}");
            else
                _logger.LogDebug($"Read response from {url}");
            return response;
        }

        public async Task<Stream> PostReadAsStreamAsync(string url, FormUrlEncodedContent formUrlEncodedContent)
        {
            _logger.LogDebug($"Reading data from Post to {url}");
            var response = await _reader.PostReadAsStreamAsync(url, formUrlEncodedContent);
            if (response == null)
                _logger.LogDebug($"No data recieved from post to {url}");
            else
                _logger.LogDebug($"Read response from {url}");
            return response;
        }
    }
}
