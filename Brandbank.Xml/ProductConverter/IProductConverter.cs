using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brandbank.Xml.ProductConverter
{
    public interface IProductConverter<T>
    {
        MessageType Convert(T item);
    }
}
