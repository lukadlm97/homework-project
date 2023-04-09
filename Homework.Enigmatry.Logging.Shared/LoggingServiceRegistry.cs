using Microsoft.Extensions.DependencyInjection;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Logging.Shared.Implementations;

namespace Homework.Enigmatry.Logging.Shared
{
    public static class LoggingServiceRegistry
    {
        public static IServiceCollection ConfigureLoggingServices(this IServiceCollection services)
        {

            services.AddSingleton<IHighPerformanceLogger,HighPerformanceLogger>();
            services.AddScoped<LogTraceData>();
            return services;
        }
    }
}
