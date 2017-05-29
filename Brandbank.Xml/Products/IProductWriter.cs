using Brandbank.Xml.Models.Message;

namespace Brandbank.Xml.Products
{
    public interface IProductWriter
    {
        void Save(ProductType product);
    }
}