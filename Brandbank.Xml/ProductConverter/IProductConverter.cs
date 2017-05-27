using Brandbank.Xml.Models.Message;

namespace Brandbank.Xml.ProductConverter
{
    public interface IProductConverter<T>
    {
        ProductType Convert(T item);
    }
}
