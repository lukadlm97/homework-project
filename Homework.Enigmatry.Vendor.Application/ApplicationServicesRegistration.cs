using Homework.Enigmatry.Application.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.Enigmatry.Vendor.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureVendorApplicationServices(this IServiceCollection services)
        {
            services.ConfigureBaseApplicationServices();

            return services;
        }
    }
}
