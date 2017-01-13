using System.IO;

namespace Brandbank.Xml.ImageWriter
{
    public class ImageWriter : IImageWriter
    {
        private readonly string _imagesDirectory;

        public ImageWriter(string imagesDirectory)
        {
            _imagesDirectory = imagesDirectory;
        }

        public void SaveToDisk(Stream imageStream, string fileName)
        {
            using (var fileStream = File.Create(fileName))
            {
                imageStream.Seek(0, SeekOrigin.Begin);
                imageStream.CopyTo(fileStream);
            }
        }
    }
}
