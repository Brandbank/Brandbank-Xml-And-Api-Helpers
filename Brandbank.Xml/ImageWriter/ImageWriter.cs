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
            var path = Path.Combine(_imagesDirectory, fileName);
            using (var fileStream = File.Create(path))
            {
                imageStream.Seek(0, SeekOrigin.Begin);
                imageStream.CopyTo(fileStream);
            }
        }
    }
}
