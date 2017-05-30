using Brandbank.Xml.Logging;
using Brandbank.Xml.Models.Message;
using Newtonsoft.Json;
using System;

namespace Brandbank.Xml.Products
{
    public class ProductWriterLogger: IProductWriter
    {
        private readonly ILogger<IProductWriter> _logger;
        private readonly IProductWriter _productWriter;

        public ProductWriterLogger(ILogger<IProductWriter> logger, IProductWriter productWriter)
        {
            _logger = logger;
            _productWriter = productWriter;
        }

        public void Save(ProductType product)
        {
            var jsonProduct = JsonConvert.SerializeObject(product);

            _logger.LogDebug($"Saving product {jsonProduct}");
            try
            {
                _productWriter.Save(product);
                _logger.LogDebug($"Saved product {jsonProduct}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Saving product {jsonProduct} failed: {e}");
                throw;
            }
        }
    }
}
