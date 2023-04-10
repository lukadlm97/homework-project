using System.Collections.Immutable;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework.Enigmatry.Persistence.Shared.Repositories.DbRepositories
{
    public class ArticleRepository:IArticleRepository
    {
        private readonly ShopDbContext _shopDbContext;

        public ArticleRepository(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }
        public async Task<Article?> Get(int id)
        {
            return await _shopDbContext.Articles.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Article>> GetAll()
        {
            return await _shopDbContext.Articles.ToListAsync();
        }

        public Task<Article> Add(Article entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(int id)
        {
            return await _shopDbContext.Articles.AnyAsync(x => x.Id == id);
        }

        public Task Update(Article entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Article entity)
        {
            throw new NotImplementedException();
        }
    }
}
