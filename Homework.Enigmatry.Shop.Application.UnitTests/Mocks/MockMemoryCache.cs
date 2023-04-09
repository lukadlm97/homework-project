using Homework.Enigmatry.Shop.Domain.Entities;
using Moq;
using System.Runtime.Caching;
using Homework.Enigmatry.Shop.Application.Extensions;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Mocks
{
    public static class MockMemoryCache
    {
        public static Mock<MemoryCache> GetMemoryCache()
        {
            var memoryCacheMock = new Mock<MemoryCache>("cache");
            var articles = new List<Article>()
            {
                new Article()
                {
                    Id = 1,
                    Name = "article 1",
                    Price = new decimal(new Random(100).NextDouble())
                },
                new Article()
                {
                    Id = 2,
                    Name = "article 2",
                    Price = new decimal(new Random(200).NextDouble())
                },
                new Article()
                {
                    Id = 3,
                    Name = "article 3",
                    Price = new decimal(new Random(300).NextDouble())
                }
            };

            foreach (var article in articles)
            {
                memoryCacheMock.Object.Add(article.Id.CreateArticleCacheKey(), article,new CacheItemPolicy()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(20)
                });
            }

            return memoryCacheMock;
        }
    }
}
