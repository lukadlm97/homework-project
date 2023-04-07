
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class OrderRepository:GenericRepository<Order>,IOrderRepository
    {
        public OrderRepository(InMemoryDbContext<Order> inMemoryDbContext) : base(inMemoryDbContext)
        {
        }
    }
}
