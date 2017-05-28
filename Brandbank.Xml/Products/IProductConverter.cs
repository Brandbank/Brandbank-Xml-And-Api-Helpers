using Brandbank.Xml.Models.Message;

namespace Brandbank.Xml.Products
{
    public interface IProductConverter<T>
    {
        ProductType Convert(T item);
    }
}
