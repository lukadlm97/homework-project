using Homework.Enigmatry.Application.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Homework.Enigmatry.Vendor.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureVendorApplicationServices(this IServiceCollection services)
        {
            services.ConfigureBaseApplicationServices();
            services.AddMediatR(Assembly.GetAssembly(typeof(ApplicationServicesRegistration)));

            return services;
        }
    }
}
