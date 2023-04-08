using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Models;
using Microsoft.Extensions.Options;

namespace Homework.Enigmatry.Shop.Infrastructure.Services.Vendor
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly VendorSettings _vendorSettings;

        public VendorService(IOptions<VendorSettings> options, IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
            _vendorSettings = options.Value;
        }
        public async Task<List<ArticleDetailsDto>> GetAvailableArticles(int id, CancellationToken cancellationToken = default)
        {
            List<ArticleDetailsDto> articles = new List<ArticleDetailsDto>();


            ArticleDetailsDto? article = null;
            if (await _vendorRepository.IsArticleExist(id, _vendorSettings.FirstVendorHttpClientName, cancellationToken))
            {
                article=
                    await _vendorRepository.GetArticle(id, _vendorSettings.FirstVendorHttpClientName, cancellationToken); 
                
                if (article != null)
                {
                    articles.Add(article);
                }
            }
            if (await _vendorRepository.IsArticleExist(id, _vendorSettings.FirstVendorHttpClientName, cancellationToken))
            {
                article =
                    await _vendorRepository.GetArticle(id, _vendorSettings.SecoundVendorHttpClientName, cancellationToken);
                if (article != null)
                {
                    articles.Add(article);
                }

            }

            return articles;
        }
    }
}
