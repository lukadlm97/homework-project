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
                    Id = 1,
                    Name = "article 1",
                    Price = new decimal(new Random(100).NextDouble())
                },
                new Article()
                {
                    Id = 5,
                    Name = "article 2",
                    Price = new decimal(new Random(200).NextDouble())
                },
                new Article()
                {
                    Id = 13,
                    Name = "article 3",
                    Price = new decimal(new Random(300).NextDouble())
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
