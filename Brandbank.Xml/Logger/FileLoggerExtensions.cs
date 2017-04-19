using Microsoft.Extensions.Logging;

namespace Brandbank.Xml.Logger
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFileLogger(this ILoggerFactory loggerFactory, string basePath)
        {
            loggerFactory.AddProvider(new FileLoggerProvider(basePath));
            return loggerFactory;
        }
    }
}
