using System.IO;

namespace Brandbank.Xml.ImageWriter
{
    public interface IImageWriter
    {
        void SaveToDisk(Stream imageStream, string path);
    }
}
