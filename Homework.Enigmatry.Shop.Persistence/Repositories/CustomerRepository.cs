using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly InMemoryDbContext _context;
        private readonly LogTraceData _logTraceData;

        public CustomerRepository(InMemoryDbContext context, LogTraceData logTraceData)
        {
            _context = context;
            _logTraceData = logTraceData;
        }
        public async Task<Customer?> Get(int id)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(Get)));

            return _context.Customers.FirstOrDefault(x => x.Id == id);
        }

        public Task<IReadOnlyList<Customer>> GetAll()
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(GetAll)));
            throw new NotImplementedException();
        }

        public async Task<Customer> Add(Customer entity)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(Add)));
            var nextId = _context.Customers.Count;
            entity.Id= nextId;

            _context.Customers.Add(entity);
            return entity;
        }

        public async Task<bool> Exists(int id)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(Exists)));
            return _context.Customers.Any(x => x.Id == id);
        }

        public Task Update(Customer entity)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(Update)));

            throw new NotImplementedException();
        }

        public Task Delete(Customer entity)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(Delete)));

            throw new NotImplementedException();
        }

        public async Task<Customer?> GetByUsername(string username, CancellationToken cancellationToken = default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(GetByUsername)));

            return _context.Customers.FirstOrDefault(x =>
                string.Compare(x.Username, username,StringComparison.InvariantCulture) == 0);
        }
    }
}
