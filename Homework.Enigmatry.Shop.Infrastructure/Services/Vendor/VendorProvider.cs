﻿using System.Text.Json;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Homework.Enigmatry.Shop.Infrastructure.Services.Vendor
{
    public class VendorProvider : IVendorProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHighPerformanceLogger _logger;
        private readonly LogTraceData _logTraceData;

        public VendorProvider(IHighPerformanceLogger logger, IHttpClientFactory clientFactory,LogTraceData logTraceData)
        {
            _logger = logger;
            _httpClientFactory = clientFactory;
            _logTraceData = logTraceData;
        }
        public async Task<ArticleDetailsDto?> GetArticle(int id, string vendorHttpClientName, CancellationToken cancellationToken = default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2},vendorHttpClientName:{3})",
                nameof(VendorProvider), nameof(GetArticle),id,vendorHttpClientName));
            try
            {
                var httpClient = _httpClientFactory.CreateClient(vendorHttpClientName);
                var httpResponseMessage = await httpClient.GetAsync(id.ToString(), cancellationToken);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var responseStr = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
                    return JsonSerializer.Deserialize<ArticleDetailsDto>(responseStr);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message,ex.InnerException,LogLevel.Error);
                return null;
            }
        }

        public async Task<bool> IsArticleExist(int id, string vendorHttpClientName, CancellationToken cancellationToken = default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2},vendorHttpClientName:{3})",
                nameof(VendorProvider), nameof(IsArticleExist), id, vendorHttpClientName));
            try
            {
                var httpClient = _httpClientFactory.CreateClient(vendorHttpClientName);
                var httpResponseMessage = await httpClient.GetAsync(id+"/exist", cancellationToken);

                return httpResponseMessage.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, ex.InnerException, LogLevel.Error);
                return false;
            }
        }
    }
}
