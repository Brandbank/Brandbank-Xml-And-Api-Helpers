using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brandbank.Xml.BbHttpClientResponseConverter
{
    public interface IBbHttpClientResponseConverter
    {
        T ConvertJsonTo<T>(Stream stream) where T : class;
        Bitmap ConvertToBitmap(Stream stream);
    }
}