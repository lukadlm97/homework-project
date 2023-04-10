using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Moq;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Mocks
{
    public static class MockOrderRepository
    {
        public static Mock<IOrderRepository> GetOrderRepositoryMock()
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    ArticleId = 11111,
                    CustomerId = 1,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Price = 20.22m
                }
            };

            var mockRepository = new Mock<IOrderRepository>();

            mockRepository.Setup(r => r.GetAll())
                .ReturnsAsync(orders);

            mockRepository.Setup(r => r
                    .ExistForArticle(It.IsAny<int>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync((int articleId,CancellationToken cancellationToken) =>
                {
                    return orders.Any(x => x.ArticleId == articleId);
                });

           

            return mockRepository;
        }
    }
}
