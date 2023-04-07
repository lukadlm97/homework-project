using System.Text.Json;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Shop.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Homework.Enigmatry.Shop.Infrastructure.Implementation
{
    public class VendorRepository:IVendorRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<VendorRepository> _logger;

        public VendorRepository(ILogger<VendorRepository> logger,IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _httpClientFactory = clientFactory;
        }
        public async Task<ArticleDto?> Get(int id, string vendorHttpClientName, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(vendorHttpClientName);
                var httpResponseMessage = await httpClient.GetAsync(id.ToString(),cancellationToken);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);
                    return await JsonSerializer.DeserializeAsync
                        <ArticleDto>(contentStream,cancellationToken:cancellationToken);
                }

                return null;
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex.Message,ex);
                }
                return null;
            }
        }
    }
}
