using Moq;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Mocks
{
    public static class MockCustomerRepository
    {
        public static Mock<ICustomerRepository> GetCustomerRepositoryMock()
        {
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Id = 1,
                    Role = Homework.Enigmatry.Shop.Application.Constants.Constants.CustomerRole
                },
                new Customer()
                {
                    Id = 2,
                    Role = Homework.Enigmatry.Shop.Application.Constants.Constants.CustomerRole
                },
                new Customer()
                {Id = 3,
                    Role = Homework.Enigmatry.Shop.Application.Constants.Constants.AdminRole}
            };

            var mockRepository = new Mock<ICustomerRepository>();

            mockRepository.Setup(r => r.GetAll())
                .ReturnsAsync(customers);

            mockRepository.Setup(r => r.Add(It.IsAny<Customer>()))
                .ReturnsAsync((Customer article) =>
                {
                    customers.Add(article);
                    return article;
                });

            mockRepository.Setup(r => r.Get(It.IsAny<int>()))
                .ReturnsAsync((int customerId) =>
                {
                    return customers.FirstOrDefault(x => x.Id == customerId);
                });

            mockRepository.Setup(r => r.Exists(It.IsAny<int>()))
                .ReturnsAsync((int customerId) =>
                {
                    return customers.Any(x => x.Id == customerId);
                });


            return mockRepository;
        }
    }
}
