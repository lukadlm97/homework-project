using System.Runtime.Caching;
using Homework.Enigmatry.Application.Shared;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Homework.Enigmatry.Shop.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureShopApplicationServices(this IServiceCollection services)
        {
            services.ConfigureBaseApplicationServices();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<MemoryCache>(new MemoryCache("cache"));

            return services;
        }

       
    }
}
