using Brandbank.Xml.Helpers;
using System.IO;

namespace Brandbank.Http.BbHttpClientResponseConverter
{
    public class BbHttpClientResponseConverter : IBbHttpClientResponseConverter
    {
        public T ConvertJsonTo<T>(Stream stream) where T : class
        {
            return stream.ConvertJsonStringTo<T>();
        }
    }
}
