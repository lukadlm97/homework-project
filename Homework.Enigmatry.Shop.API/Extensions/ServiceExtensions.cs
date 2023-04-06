using Homework.Enigmatry.Shop.Application.Models;

namespace Homework.Enigmatry.Shop.API.Extensions
{
    public static class ServiceExtensions
    {
       public static void ConfigureVendorsHttpClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            string? httpClientName = configuration.GetSection($"{nameof(VendorSettings)}:FirstVendorHttpClientName").Value;
            services.AddHttpClient(
                httpClientName ?? "",
                client =>
                {
                    // Set the base address of the named client.
                    client.BaseAddress = new Uri(configuration.GetSection($"{nameof(VendorSettings)}:FirstVendorUrl").Value);

                    // Add a user-agent default request header.
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
                });

            httpClientName = configuration.GetSection($"{nameof(VendorSettings)}:SecoundVendorHttpClientName").Value;
            services.AddHttpClient(
                httpClientName ?? "",
                client =>
                {
                    // Set the base address of the named client.
                    client.BaseAddress = new Uri(configuration.GetSection($"{nameof(VendorSettings)}:SecoundVendorUrl").Value);

                    // Add a user-agent default request header.
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
                });

        }
    }
}
