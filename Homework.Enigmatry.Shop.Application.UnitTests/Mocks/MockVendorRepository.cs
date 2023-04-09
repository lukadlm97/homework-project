using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework.Enigmatry.Application.Shared.Contracts;
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

            var mockRepository = new Mock<IVendorRepository>();

            mockRepository.Setup(r => r
                    .IsArticleExist(It.IsAny<int>(),"asd",It.IsAny<CancellationToken>()))
                .ReturnsAsync((int articleId, string vendorName, CancellationToken cancellationToken) =>
                {
                    return articles.Any(x=>x.Id==articleId);
                });

            mockRepository.Setup(r => r
                    .GetArticle(It.IsAny<int>(), "asd", It.IsAny<CancellationToken>()))
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
