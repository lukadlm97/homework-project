using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.Enigmatry.Shop.Persistence
{
    public static class ServicePersistenceRegistration
    {
        public static IServiceCollection ConfigureInMemoryPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddSingleton(typeof(InMemoryDbContext<>));
            services.AddSingleton(typeof(InMemoryDbContext));


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
        public static IServiceCollection ConfigureInMemoryVendorPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(InMemoryDbContext));
            
            services.AddScoped<IArticleRepository, ArticleRepository>();

            return services;
        }
    }
   
}
