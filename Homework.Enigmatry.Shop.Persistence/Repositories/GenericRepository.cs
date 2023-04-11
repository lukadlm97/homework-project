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

        public async Task Update(T entity)
        {
            var item = _inMemoryDbContext.List.First(x => x.Id == entity.Id);
            _inMemoryDbContext.List.Remove(item);
            _inMemoryDbContext.List.Add(entity);
        }

        public async Task Delete(T entity)
        {
            _inMemoryDbContext.List.Remove(entity);
        }
    }
}
