using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Article;
using Homework.Enigmatry.Shop.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
                        <ArticleDto>(contentStream);
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
