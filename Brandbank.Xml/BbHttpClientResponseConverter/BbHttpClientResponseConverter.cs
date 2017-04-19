using Brandbank.Xml.Helpers;
using System.Drawing;
using System.IO;

namespace Brandbank.Xml.BbHttpClientResponseConverter
{
    public class BbHttpClientResponseConverter : IBbHttpClientResponseConverter
    {
        public T ConvertJsonTo<T>(Stream stream) where T : class
        {
            return stream.ConvertJsonStringTo<T>();
        }

        public Bitmap ConvertToBitmap(Stream stream)
        {
            return new Bitmap(stream);
        }
    }
}
