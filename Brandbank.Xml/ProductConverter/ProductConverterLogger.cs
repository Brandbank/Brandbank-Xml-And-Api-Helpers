using Brandbank.Xml.Models.Message;
using Microsoft.Extensions.Logging;

namespace Brandbank.Xml.ProductConverter
{
    public class ProductConverterLogger<T> : IProductConverter<T>
    {
        private readonly ILogger<IProductConverter<T>> _logger;
        private readonly IProductConverter<T> _productConverter;

        public ProductConverterLogger(ILogger<IProductConverter<T>> logger, IProductConverter<T> productConverter)
        {
            _logger = logger;
            _productConverter = productConverter;
        }

        public MessageType Convert(T item)
        {
            _logger.LogDebug($"Converting data to Brandbank Message");
            var result = _productConverter.Convert(item);
            _logger.LogDebug($"Converted data to Brandbank Message");
            return result;
        }
    }
}
