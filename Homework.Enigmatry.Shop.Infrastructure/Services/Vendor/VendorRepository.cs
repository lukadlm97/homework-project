using System.Text.Json;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Shop.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Homework.Enigmatry.Shop.Infrastructure.Services.Vendor
{
    public class VendorRepository : IVendorRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<VendorRepository> _logger;

        public VendorRepository(ILogger<VendorRepository> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _httpClientFactory = clientFactory;
        }
        public async Task<ArticleDetailsDto?> GetArticle(int id, string vendorHttpClientName, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(vendorHttpClientName);
                var httpResponseMessage = await httpClient.GetAsync(id.ToString(), cancellationToken);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);
                    return await JsonSerializer.DeserializeAsync
                        <ArticleDetailsDto>(contentStream, cancellationToken: cancellationToken);
                }

                return null;
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex.Message, ex);
                }
                return null;
            }
        }

        public async Task<bool> IsArticleExist(int id, string vendorHttpClientName, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(vendorHttpClientName);
                var httpResponseMessage = await httpClient.GetAsync(id+"/exist", cancellationToken);

                return httpResponseMessage.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex.Message, ex);
                }
                return false;
            }
        }
    }
}
