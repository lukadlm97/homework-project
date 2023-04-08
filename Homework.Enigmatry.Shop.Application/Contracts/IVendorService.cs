using Homework.Enigmatry.Application.Shared.DTOs.Article;

namespace Homework.Enigmatry.Shop.Application.Contracts
{
    public interface IVendorService
    {
        Task<List<ArticleDetailsDto>> Get(int id,CancellationToken cancellationToken=default);
    }
}
