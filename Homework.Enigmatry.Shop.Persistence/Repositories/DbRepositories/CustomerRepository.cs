
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Homework.Enigmatry.Persistence.Shared.Repositories.DbRepositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly ShopDbContext _shopDbContext;

        public CustomerRepository(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }
        public async Task<Customer?> Get(int id)
        {
            return await _shopDbContext.Customers
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<IReadOnlyList<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> Add(Customer entity)
        {
            await _shopDbContext.Customers.AddAsync(entity);
            return entity;
        }

        public async Task<bool> Exists(int id)
        {
            return await _shopDbContext.Customers.AnyAsync(x =>x.Id==id);
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
            return await _shopDbContext.Customers.FirstOrDefaultAsync(x =>x.Username== username,cancellationToken);
        }
    }
}
