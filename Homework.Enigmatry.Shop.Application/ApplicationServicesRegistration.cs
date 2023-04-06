
using System.Runtime.Caching;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Homework.Enigmatry.Shop.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<MemoryCache>();

            return services;
        }
    }
}
