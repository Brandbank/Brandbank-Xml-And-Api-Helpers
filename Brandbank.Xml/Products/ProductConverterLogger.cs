using Brandbank.Xml.Logging;
using Brandbank.Xml.Models.Message;
using Newtonsoft.Json;
using System;

namespace Brandbank.Xml.Products
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

        public ProductType Convert(T item)
        {
            var jsonItem = JsonConvert.SerializeObject(item);

            _logger.LogDebug($"Converting product {jsonItem}");
            try
            {
                var product = _productConverter.Convert(item);
                _logger.LogDebug($"Converted product {jsonItem} to {JsonConvert.SerializeObject(product)}");
                return product;
            }
            catch (Exception e)
            {
                _logger.LogError($"Converting product {jsonItem} failed: {e}");
                throw;
            }
        }
    }
}
