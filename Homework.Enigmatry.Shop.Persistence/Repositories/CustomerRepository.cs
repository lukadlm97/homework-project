
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
        public Task<Customer?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
