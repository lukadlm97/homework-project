using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Logging.Shared.Implementations;
using Homework.Enigmatry.Shop.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Homework.Enigmatry.Persistence.Shared.Repositories.DbRepositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private  IArticleRepository _articleRepository;
        private  ICustomerRepository _customerRepository;
        private  IOrderRepository _orderRepository;
        private readonly ShopDbContext _context;
        private readonly LogTraceData _logTraceData;
        private readonly IHighPerformanceLogger _highPerformanceLogger;

        public UnitOfWork(ShopDbContext shopDbContext,LogTraceData logTraceData,IHighPerformanceLogger highPerformanceLogger)
        {
            _context = shopDbContext;
            _logTraceData = logTraceData;
            _highPerformanceLogger = highPerformanceLogger;
        }

        public IArticleRepository ArticleRepository =>
            _articleRepository ??= new ArticleRepository(_context);
        public ICustomerRepository CustomerRepository =>
            _customerRepository ??= new CustomerRepository(_context);
        public IOrderRepository OrderRepository =>
            _orderRepository ??= new OrderRepository(_context);
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

      
        public async Task Save()
        {
            try
            {
                _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(UnitOfWork), nameof(Save)));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _highPerformanceLogger.Log(ex.Message,ex,LogLevel.Error);
                throw;
            }
     
        }
    }
}
