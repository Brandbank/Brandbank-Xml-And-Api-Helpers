using System.IO;

namespace Brandbank.Http.BbHttpClientResponseConverter
{
    public interface IBbHttpClientResponseConverter
    {
        T ConvertJsonTo<T>(Stream stream) where T : class;
    }
}