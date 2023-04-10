using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Moq;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Mocks
{
    public static class MockVendorRepository
    {
        public static Mock<IVendorRepository> GetVendorRepositoryMock()
        {
            var articles = new List<Article>()
            {
                new Article()
                {
                    Id = 111,
                    Name = string.Format("{0} {1}",Constants.Constants.ArticleName,111),
                    Price = 2000
                },
                new Article()
                {
                    Id = 112,
                    Name = string.Format("{0} {1}",Constants.Constants.ArticleName,112),
                    Price =2000
                },
                new Article()
                {
                    Id = 113,
                    Name = string.Format("{0} {1}",Constants.Constants.ArticleName,113),
                    Price = 2000
                }
            };

            var mockRepository = new Mock<IVendorRepository>();

            mockRepository.Setup(r => r
                    .IsArticleExist(It.IsAny<int>(),It.IsAny<string>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync((int articleId, string vendorName, CancellationToken cancellationToken) =>
                {
                    return articles.Any(x=>x.Id==articleId);
                });

            mockRepository.Setup(r => r
                    .GetArticle(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int articleId,string vendorName,CancellationToken cancellationToken) =>
                {
                    var selectedArticle = articles.FirstOrDefault(x => x.Id == articleId);
                    if (selectedArticle != null)
                    {
                        return new ArticleDetailsDto(selectedArticle.Id, selectedArticle.Name, selectedArticle.Price);
                    }

                    return null;
                });

           


            return mockRepository;
        }
    }
}
