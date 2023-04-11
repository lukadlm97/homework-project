using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework.Enigmatry.Persistence.Shared.Repositories.DbRepositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ShopDbContext _shopDbContext;

        public OrderRepository(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }
    
        public Task<Order?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Order>> GetAll()
        {
            return await _shopDbContext.Orders
                .Include(x=>x.Article)
                .Include(x=>x.Customer)
                .ToListAsync();
        }

        public async Task<Order> Add(Order entity)
        {
             await _shopDbContext.Orders.AddAsync(entity);
             return entity;
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistForArticle(int articleId, CancellationToken cancellationToken = default)
        {
            return await _shopDbContext.Orders.AnyAsync(x => x.ArticleId == articleId && !x.IsDeleted,cancellationToken);
        }
    }
}
