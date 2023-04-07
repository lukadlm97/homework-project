using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Shop.Persistence.Repositories
{
    public class ArticleRepository:IArticleRepository
    {
        private readonly InMemoryDbContext _inMemoryDbContext;

        public ArticleRepository(InMemoryDbContext inMemoryDbContext)
        {
            _inMemoryDbContext = inMemoryDbContext;
        }
        public async Task<Article?> Get(int id)
        {
            return _inMemoryDbContext.Articles.SingleOrDefault(x=>x.Id==id);
        }

        public async Task<IReadOnlyList<Article>> GetAll()
        {
            return _inMemoryDbContext.Articles;
        }

        public async Task<Article> Add(Article entity)
        {
            _inMemoryDbContext.Articles.Add(entity);
            return entity;
        }

        public async Task<bool> Exists(int id)
        {
            var article = _inMemoryDbContext.Articles.SingleOrDefault(x => x.Id == id);
            return article != null;
        }

        public async Task Update(Article entity)
        {
            var item = _inMemoryDbContext.Articles.SingleOrDefault(x => x.Id == entity.Id);
            item.Id=entity.Id;
            item.IsSold=entity.IsSold;
            item.Name=entity.Name;
            item.SoldDate=entity.SoldDate;
        }

        public async Task Delete(Article entity)
        {
            _inMemoryDbContext.Articles.Remove(entity);
        }
    }
}
