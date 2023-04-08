using Homework.Enigmatry.Shop.Application.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Homework.Enigmatry.Shop.Presentation.Swagger
{
    public static class ServiceSwaggerRegistration
    {
        public static IServiceCollection ConfigureSwaggerServices(this IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                c.SwaggerDoc(configuration.GetSection("ApplicationSettings:Version").Value, new OpenApiInfo
                {
                    Version = configuration.GetSection("ApplicationSettings:Version").Value,
                    Title = configuration.GetSection("ApplicationSettings:Title").Value,

                });

            });
            return services;
        }
    }
}
