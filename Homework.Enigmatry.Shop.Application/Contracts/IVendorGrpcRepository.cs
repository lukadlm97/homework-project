using Homework.Enigmatry.Application.Shared.DTOs.Article;

namespace Homework.Enigmatry.Shop.Application.Contracts
{
    public interface IVendorGrpcRepository
    {
        Task<ArticleDetailsDto?> GetArticle(int id, CancellationToken cancellationToken = default);
        Task<bool> IsArticleExist(int id,CancellationToken cancellationToken = default);
    }
}
