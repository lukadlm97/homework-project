using Microsoft.Extensions.Logging;

namespace Homework.Enigmatry.Logging.Shared.Contracts
{
    public interface IHighPerformanceLogger
    {
        void Log(string message, LogLevel logLevel);
        void Log(string message,Exception innerException,LogLevel logLevel);
    }
}
