using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private InMemoryDbContext _context;
        private IArticleRepository _articleRepository;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private readonly InMemoryDbContext<Order> _orderContext;
        private readonly LogTraceData _logTraceData;


        public UnitOfWork(InMemoryDbContext context,InMemoryDbContext<Order> orderContext, IHttpContextAccessor httpContextAccessor,LogTraceData logTraceData)
        {
            _context = context;
            _orderContext = orderContext;
            this._httpContextAccessor = httpContextAccessor;
            _logTraceData = logTraceData;
        }

        public IArticleRepository ArticleRepository =>
            _articleRepository ??= new ArticleRepository(_context, _logTraceData);
        public ICustomerRepository CustomerRepository =>
            _customerRepository ??= new CustomerRepository(_context, _logTraceData);
        public IOrderRepository OrderRepository =>
            _orderRepository ??= new OrderRepository(_orderContext, _logTraceData);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(UnitOfWork), nameof(Save)));
            //var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

            //await _context.SaveChangesAsync(username);
        }
    }
}
