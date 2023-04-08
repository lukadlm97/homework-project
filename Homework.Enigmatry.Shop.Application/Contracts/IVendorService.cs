using Homework.Enigmatry.Application.Shared.DTOs.Article;

namespace Homework.Enigmatry.Shop.Application.Contracts
{
    public interface IVendorService
    {
        Task<List<ArticleDetailsDto>> GetAvailableArticles(int id,CancellationToken cancellationToken=default);
    }
}
