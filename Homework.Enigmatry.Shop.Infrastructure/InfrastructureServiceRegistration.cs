﻿using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Infrastructure.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.Enigmatry.Shop.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IVendorService, VendorService>();

            return services;
        }
    }
}