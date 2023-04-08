using System.Reflection;
using System.Runtime.Caching;
using FluentValidation;
using Homework.Enigmatry.Application.Shared;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Homework.Enigmatry.Shop.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureShopApplicationServices(this IServiceCollection services)
        {
            services.ConfigureBaseApplicationServices();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationServicesRegistration)));
            services.AddMediatR(Assembly.GetAssembly(typeof(ApplicationServicesRegistration)));
            services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ApplicationServicesRegistration)));
            services.AddSingleton<MemoryCache>(new MemoryCache("cache"));


            return services;
        }

       
    }
}
