using Microsoft.Extensions.Logging;

namespace Brandbank.Xml.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _basePath;

        public FileLoggerProvider(string basePath)
        {
            _basePath = basePath;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName, _basePath);
        }

        public void Dispose()
        {
        }
    }
}
