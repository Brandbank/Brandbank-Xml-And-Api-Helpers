using Brandbank.Xml.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.IO;

namespace Brandbank.Xml.BbHttpClientResponseConverter
{
    public class BbHttpClientResponseConverterLogger : IBbHttpClientResponseConverter
    {
        private readonly ILogger _logger;
        private readonly IBbHttpClientResponseConverter _converter;

        public BbHttpClientResponseConverterLogger(ILogger logger, IBbHttpClientResponseConverter converter)
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

        public Bitmap ConvertToBitmap(Stream stream)
        {
            _logger.LogDebug("Converting image stream to Bitmap");
            try
            {
                var response = _converter.ConvertToBitmap(stream);
                _logger.LogDebug("Converted image stream to Bitmap");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to image stream to Bitmap {e.Message} {e.Source}");
                throw;
            }
        }
    }
}
