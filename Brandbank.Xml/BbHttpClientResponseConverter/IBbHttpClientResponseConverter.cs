using System.Drawing;
using System.IO;

namespace Brandbank.Xml.BbHttpClientResponseConverter
{
    public interface IBbHttpClientResponseConverter
    {
        T ConvertJsonTo<T>(Stream stream) where T : class;
        Bitmap ConvertToBitmap(Stream stream);
    }
}