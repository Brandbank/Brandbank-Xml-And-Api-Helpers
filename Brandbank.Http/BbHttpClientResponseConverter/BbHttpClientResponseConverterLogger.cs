using Brandbank.Xml.Helpers;
using Brandbank.Xml.Logging;
using System;
using System.IO;

namespace Brandbank.Http.BbHttpClientResponseConverter
{
    public class BbHttpClientResponseConverterLogger : IBbHttpClientResponseConverter
    {
        private readonly ILogger<IBbHttpClientResponseConverter> _logger;
        private readonly IBbHttpClientResponseConverter _converter;

        public BbHttpClientResponseConverterLogger(ILogger<IBbHttpClientResponseConverter> logger, IBbHttpClientResponseConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }

        public T ConvertJsonTo<T>(Stream stream) where T : class
        {
            _logger.LogDebug("Converting data stream");
            try
            {
                var response = _converter.ConvertJsonTo<T>(stream);
                _logger.LogDebug($"Converted data stream to: {response.ToJson()}");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to convert data stream {e.Message} {e.Source}");
                throw;
            }
        }
    }
}
