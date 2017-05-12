using Brandbank.Xml.Models.Message;

namespace Brandbank.Xml.ProductConverter
{
    public interface IProductConverter<in T>
    {
        ProductType Convert(T item);
    }
}
