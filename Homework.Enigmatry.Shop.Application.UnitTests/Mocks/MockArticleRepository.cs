using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Moq;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Mocks
{
    public static class MockArticleRepository
    {
        public static Mock<IArticleRepository> GetArticleRepository()
        {
            var articles = new List<Article>()
            {
                new Article()
                {
                    Id = 1,
                    Name = string.Format("{0} {1}",Constants.Constants.ArticleName,1),
                    Price = 2000
                },
                new Article()
                {
                    Id = 2,
                    Name = string.Format("{0} {1}",Constants.Constants.ArticleName,2),
                    Price = 2000
                },
                new Article()
                {
                    Id = 3,
                    Name = string.Format("{0} {1}",Constants.Constants.ArticleName,3),
                    Price = 2000
                }
            };

            var mockRepository = new Mock<IArticleRepository>();

            mockRepository.Setup(r => r.GetAll())
                .ReturnsAsync(articles);

            mockRepository.Setup(r => r.Add(It.IsAny<Article>()))
                .ReturnsAsync((Article article) =>
                {
                    articles.Add(article);
                    return article;
                });

            mockRepository.Setup(r => r.Get(It.IsAny<int>()))
                .ReturnsAsync((int articleId) =>
                {
                    return articles.FirstOrDefault(x => x.Id == articleId);
                });

            mockRepository.Setup(r => r.Exists(It.IsAny<int>()))
                .ReturnsAsync((int articleId) =>
                {
                    return articles.Any(x => x.Id == articleId);
                });


            return mockRepository;
        }
    }
}
