using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Homework.Enigmatry.Shop.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private InMemoryDbContext _context;
        private IArticleRepository _articleRepository;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private readonly InMemoryDbContext<Order> _orderContext;


        public UnitOfWork(InMemoryDbContext context,InMemoryDbContext<Order> orderContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _orderContext = orderContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IArticleRepository ArticleRepository =>
            _articleRepository ??= new ArticleRepository(_context);
        public ICustomerRepository CustomerRepository =>
            _customerRepository ??= new CustomerRepository(_context);
        public IOrderRepository OrderRepository =>
            _orderRepository ??= new OrderRepository(_orderContext);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            //var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

            //await _context.SaveChangesAsync(username);
        }
    }
}
