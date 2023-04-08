
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class OrderRepository:IOrderRepository
    {
        private readonly InMemoryDbContext<Order> _inMemoryDbContext;

        public OrderRepository(InMemoryDbContext<Order> inMemoryDbContext)
        {
            _inMemoryDbContext = inMemoryDbContext;
        }


        public Task<Order?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> Add(Order entity)
        {
            var nextId = _inMemoryDbContext.List.Count;
            entity.Id = nextId;

            _inMemoryDbContext.List.Add(entity);
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
    }
}
