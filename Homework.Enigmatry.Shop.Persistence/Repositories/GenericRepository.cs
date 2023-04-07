using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class GenericRepository<T>:IGenericRepository<T> where T : BaseEntity
    {
        private readonly InMemoryDbContext<T> _inMemoryDbContext;

        public GenericRepository(InMemoryDbContext<T> inMemoryDbContext)
        {
            _inMemoryDbContext = inMemoryDbContext;
        }
        public async Task<T?> Get(int id)
        {
            return _inMemoryDbContext.List.SingleOrDefault(x=>x.Id==id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return _inMemoryDbContext.List;
        }

        public async Task<T> Add(T entity)
        {
            _inMemoryDbContext.List.Add(entity);
            return entity;
        }

        public async Task<bool> Exists(int id)
        {
            return _inMemoryDbContext.List.SingleOrDefault(x => x.Id == id)!=null;
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(T entity)
        {
            _inMemoryDbContext.List.Remove(entity);
        }
    }
}
