using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.Enigmatry.Persistence.Shared
{
    public static class ServicePersistenceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services,IConfiguration configuration)
        {
            var inMemoryContext =
                bool.TryParse(configuration
                    .GetSection("PersistenceSettings:UseInMemory")
                    .Value, out bool result)
                    ? result : true;

            if (inMemoryContext)
            {
                services.ConfigureInMemoryPersistenceServices(configuration);
                return services;
            }
            services.ConfigureSqlPersistenceServices(configuration);
            return services;
        }

        private static IServiceCollection ConfigureInMemoryPersistenceServices(this IServiceCollection services, IConfiguration configuration)
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

        private static IServiceCollection ConfigureSqlPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopDbContext>(options =>
                options
                    .UseSqlServer(configuration.GetSection("PersistenceSettings")["Database"]));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IArticleRepository,Repositories.DbRepositories.ArticleRepository>();
            services.AddScoped<ICustomerRepository, Repositories.DbRepositories.CustomerRepository>();
            services.AddScoped<IOrderRepository, Repositories.DbRepositories.OrderRepository>();
            services.AddScoped<IUnitOfWork, Repositories.DbRepositories.UnitOfWork>();

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
