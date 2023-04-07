using Homework.Enigmatry.Vendor.Application.Contracts;
using Homework.Enigmatry.Vendor.Infrastructure.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.Enigmatry.Vendor.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureVendorInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ISalesAgent, SalesAgent>();

            return services;
        }
    }
}
