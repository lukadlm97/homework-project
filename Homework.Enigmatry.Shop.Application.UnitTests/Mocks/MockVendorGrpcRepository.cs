using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Moq;
using Homework.Enigmatry.Application.Shared.DTOs.Article;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Mocks
{
    public static class MockVendorGrpcRepository
    {
        public static Mock<IVendorGrpcRepository> GetVendorRepositoryMock()
        {
            var articles = new List<Article>()
            {
                new Article()
                {
                    Id = 1111,
                    Name = string.Format("{0} {1}", Constants.Constants.ArticleName, 1111),
                    Price = 2000
                },
                new Article()
                {
                    Id = 1112,
                    Name = string.Format("{0} {1}", Constants.Constants.ArticleName, 1112),
                    Price = 2000
                },
                new Article()
                {
                    Id = 1113,
                    Name = string.Format("{0} {1}", Constants.Constants.ArticleName, 1113),
                    Price = 2000
                }
            };

            var mockRepository = new Mock<IVendorGrpcRepository>();

            mockRepository.Setup(r => r
                    .IsArticleExist(It.IsAny<int>(),  It.IsAny<CancellationToken>()))
                .ReturnsAsync((int articleId, CancellationToken cancellationToken) =>
                {
                    var isExist= articles.Any(x => x.Id == articleId);
                    return isExist;
                });

            mockRepository.Setup(r => r
                    .GetArticle(It.IsAny<int>(),  It.IsAny<CancellationToken>()))
                .ReturnsAsync((int articleId,CancellationToken cancellationToken) =>
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
