using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Infrastructure.Factories.Contract;
using Homework.Enigmatry.Shop.Infrastructure.Factories.Implementation;
using Homework.Enigmatry.Shop.Infrastructure.Services.Token;
using Homework.Enigmatry.Shop.Infrastructure.Services.Vendor;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.Enigmatry.Shop.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IVendorProvider, VendorProvider>();
            services.AddScoped<IVendorGrpcProvider, VendorGrpcProvider>();
            services.AddScoped<IVendorGrpcFactory, VendorGrpcFactory>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
