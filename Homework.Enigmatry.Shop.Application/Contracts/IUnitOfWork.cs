
using Homework.Enigmatry.Application.Shared.Contracts;

namespace Homework.Enigmatry.Shop.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository ArticleRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task Save();
    }
}
