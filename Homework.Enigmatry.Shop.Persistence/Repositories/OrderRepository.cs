
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class OrderRepository:IOrderRepository
    {
        private readonly InMemoryDbContext<Order> _inMemoryDbContext;
        private readonly LogTraceData _logTraceData;

        public OrderRepository(InMemoryDbContext<Order> inMemoryDbContext,LogTraceData logTraceData)
        {
            _inMemoryDbContext = inMemoryDbContext;
            _logTraceData = logTraceData;
        }


        public Task<Order?> Get(int id)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(OrderRepository), nameof(Get)));
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetAll()
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(OrderRepository), nameof(GetAll)));
            throw new NotImplementedException();
        }

        public async Task<Order> Add(Order entity)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(OrderRepository), nameof(Add)));
            var nextId = _inMemoryDbContext.List.Count+1;
            entity.Id = nextId;

            _inMemoryDbContext.List.Add(entity);
            return entity;
        }

        public Task<bool> Exists(int id)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(OrderRepository), nameof(Exists)));
            throw new NotImplementedException();
        }

        public Task Update(Order entity)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(OrderRepository), nameof(Update)));
            throw new NotImplementedException();
        }

        public Task Delete(Order entity)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(OrderRepository), nameof(Delete)));
            throw new NotImplementedException();
        }

        public async Task<bool> ExistForArticle(int articleId, CancellationToken cancellationToken = default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(OrderRepository), nameof(ExistForArticle)));
            return _inMemoryDbContext.List.Any(x => x.ArticleId == articleId && !x.IsDeleted);
        }
    }
}
