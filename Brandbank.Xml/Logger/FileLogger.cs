using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Brandbank.Xml.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly string _path;

        public FileLogger(string categoryName, string basePath)
        {
            _path = Path.Combine(basePath, "log.log");
            _categoryName = categoryName;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var log = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}|{logLevel}|{_categoryName}|{formatter(state, exception)}";
            using (var writer = File.AppendText(_path))
                writer.WriteLine(log);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
