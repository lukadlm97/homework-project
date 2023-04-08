using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly InMemoryDbContext _context;

        public CustomerRepository(InMemoryDbContext context)
        {
            _context = context;
        }
        public async Task<Customer?> Get(int id)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == id);
        }

        public Task<IReadOnlyList<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> Add(Customer entity)
        {
            var nextId = _context.Customers.Count;
            entity.Id= nextId;

            _context.Customers.Add(entity);
            return entity;
        }

        public async Task<bool> Exists(int id)
        {
            return _context.Customers.Any(x => x.Id == id);
        }

        public Task Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer?> GetByUsername(string username, CancellationToken cancellationToken = default)
        {
            return _context.Customers.FirstOrDefault(x =>
                string.Compare(x.Username, username,StringComparison.InvariantCulture) == 0);
        }
    }
}
