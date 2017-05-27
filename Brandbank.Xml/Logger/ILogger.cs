using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Logger
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
