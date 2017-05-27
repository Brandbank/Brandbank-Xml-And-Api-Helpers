namespace Brandbank.Xml.Logging
{
    public interface ILogger<T>
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogInformation(string message);
        void LogCritical(string message);
        void LogWarning(string message);
    }
}
