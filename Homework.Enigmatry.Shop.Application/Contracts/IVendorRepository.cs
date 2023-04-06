using Homework.Enigmatry.Shop.Application.DTOs.Article;

namespace Homework.Enigmatry.Shop.Application.Contracts
{
    public interface IVendorRepository
    {
        Task<ArticleDto?> Get(int id,string vendorHttpClientName,CancellationToken cancellationToken=default);
    }
}
