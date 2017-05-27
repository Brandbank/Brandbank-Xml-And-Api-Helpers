using Brandbank.Xml.Logging;
using System;
using System.IO;

namespace Brandbank.Xml.ImageWriter
{
    public class ImageWriterLogger : IImageWriter
    {
        readonly ILogger<IImageWriter> _logger;
        readonly IImageWriter _imageWriter;

        public ImageWriterLogger(ILogger<IImageWriter> logger, IImageWriter imageWriter)
        {
            _logger = logger;
            _imageWriter = imageWriter;
        }

        public void SaveToDisk(Stream imageStream, string path)
        {
            _logger.LogDebug($"Saving image to {path}");
            try
            {
                _imageWriter.SaveToDisk(imageStream, path);
                _logger.LogDebug($"Saved image to {path}");
            }
            catch (Exception)
            {
                _logger.LogError($"Saving image to {path} failed");
                throw;
            }
        }
    }
}
