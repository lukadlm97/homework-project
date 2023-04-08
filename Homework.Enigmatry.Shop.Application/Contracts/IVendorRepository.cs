using Homework.Enigmatry.Application.Shared.DTOs.Article;

namespace Homework.Enigmatry.Shop.Application.Contracts
{
    public interface IVendorRepository
    {
        Task<ArticleDetailsDto?> GetArticle(int id,string vendorHttpClientName,CancellationToken cancellationToken=default);
        Task<bool> IsArticleExist(int id,string vendorHttpClientName,CancellationToken cancellationToken=default);
    }
}
