using Homework.Enigmatry.Shop.Domain.Entities;
using Moq;
using System.Runtime.Caching;
using Homework.Enigmatry.Shop.Application.Extensions;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Mocks
{
    public static class MockMemoryCache
    {
        public static MemoryCache GetMemoryCache()
        {
            var memoryCacheMock = new MemoryCache("cache");
            var articles = new List<Article>()
            {
                new Article()
                {
                    Id = 11,
                    Name = string.Format("{0} {1}",Constants.Constants.ArticleName,11),
                    Price = 2000
                },
                new Article()
                {
                    Id = 12,
                    Name = string.Format("{0} {1}", Constants.Constants.ArticleName, 12),
                    Price = 2000
                },
                new Article()
                {
                    Id = 13,
                    Name = string.Format("{0} {1}",Constants.Constants.ArticleName,13),
                    Price = 2000
                }, new Article()
                {
                Id = 11111,
                Name = string.Format("{0} {1}",Constants.Constants.ArticleName,11),
                Price = 2000
            }
            };

            foreach (var article in articles)
            {
                memoryCacheMock.Add(article.Id.CreateArticleCacheKey(), article,new CacheItemPolicy()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(20)
                });
            }

            return memoryCacheMock;
        }
    }
}
