using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class ArticleRepository:IArticleRepository
    {
        private readonly InMemoryDbContext _inMemoryDbContext;
        private readonly LogTraceData _logTraceData;

        public ArticleRepository(InMemoryDbContext inMemoryDbContext,LogTraceData logTraceData)
        {
            _inMemoryDbContext = inMemoryDbContext;
            _logTraceData = logTraceData;
        }
        public async Task<Article?> Get(int id)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}",nameof(ArticleRepository),nameof(Get)));

            return _inMemoryDbContext.Articles.SingleOrDefault(x=>x.Id==id);
        }

        public async Task<IReadOnlyList<Article>> GetAll()
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(GetAll)));

            return _inMemoryDbContext.Articles;
        }

        public async Task<Article> Add(Article entity)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(Add)));

            _inMemoryDbContext.Articles.Add(entity);
            return entity;
        }

        public async Task<bool> Exists(int id)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(Exists)));

            var article = _inMemoryDbContext.Articles.SingleOrDefault(x => x.Id == id);
            return article != null;
        }

        public async Task Update(Article entity)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(Update)));

            var item = _inMemoryDbContext.Articles.SingleOrDefault(x => x.Id == entity.Id);
            if (item != null)
            {
                item.Id = entity.Id;
                item.Name = entity.Name;
            }
        }

        public async Task Delete(Article entity)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(ArticleRepository), nameof(Delete)));

            _inMemoryDbContext.Articles.Remove(entity);
        }
    }
}
