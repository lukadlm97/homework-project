using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.Enigmatry.Application.Shared
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureBaseApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationServicesRegistration)));
            services.AddMediatR(Assembly.GetAssembly(typeof(ApplicationServicesRegistration)));

            return services;
        }

    }
}
