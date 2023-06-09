﻿using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Models;
using Microsoft.Extensions.Options;

namespace Homework.Enigmatry.Shop.Infrastructure.Services.Vendor
{
    public class VendorService : IVendorService
    {
        private readonly IVendorProvider _vendorRepository;
        private readonly VendorSettings _vendorSettings;
        private readonly IVendorGrpcProvider _vendorGrpcRepository;
        private readonly LogTraceData _logTraceData;

        public VendorService(IOptions<VendorSettings> options, IVendorProvider vendorRepository,IVendorGrpcProvider vendorGrpcRepository,LogTraceData logTraceData)
        {
            _vendorRepository = vendorRepository;
            _vendorGrpcRepository = vendorGrpcRepository;
            _vendorSettings = options.Value;
            _logTraceData = logTraceData;
        }
        public async Task<List<ArticleDetailsDto>> GetAvailableArticles(int id, CancellationToken cancellationToken = default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} ",
                nameof(VendorService), nameof(GetAvailableArticles)));

            List<ArticleDetailsDto> articles = new List<ArticleDetailsDto>();


            ArticleDetailsDto? article = await GetArticleFromVendor(id,_vendorSettings.FirstVendorHttpClientName,cancellationToken);
            if (article != null)
            {
                articles.Add(article);
            }
            article = await GetArticleFromVendor(id, _vendorSettings.SecoundVendorHttpClientName, cancellationToken); 
            if (article != null)
            {
                articles.Add(article);
            }
          

            if (await _vendorGrpcRepository.IsArticleExist(id, cancellationToken))
            {
                article =
                    await _vendorGrpcRepository.GetArticle(id, cancellationToken);
                if (article != null)
                {
                    articles.Add(article);
                }
            }

            return articles;
        }

        private async Task<ArticleDetailsDto?> GetArticleFromVendor(int id, 
            string clientName,
            CancellationToken cancellationToken)
        {
            ArticleDetailsDto? article= null;
            if (await _vendorRepository.IsArticleExist(id, clientName, cancellationToken))
            {
                article =
                    await _vendorRepository.GetArticle(id, clientName, cancellationToken);

               
            }

            return article;
        }
    }
}
