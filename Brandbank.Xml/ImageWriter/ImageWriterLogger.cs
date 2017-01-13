using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Brandbank.Xml.ImageWriter
{
    public class ImageWriterLogger : IImageWriter
    {
        ILogger<IImageWriter> _logger;
        IImageWriter _imageWriter;

        public ImageWriterLogger(ILogger<IImageWriter> logger, IImageWriter imageWriter)
        {
            _logger = logger;
            _imageWriter = imageWriter;
        }

        public void SaveToDisk(Stream imageStream, string path)
        {
            _logger.LogDebug($"Saving image to {path}", path);
            try
            {
                _imageWriter.SaveToDisk(imageStream, path);
                _logger.LogDebug($"Saved image to {path}", path);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, $"Saving image to {path} failed", path);
                throw new Exception($"Saving image to {path} failed");
            }
        }
    }
}
