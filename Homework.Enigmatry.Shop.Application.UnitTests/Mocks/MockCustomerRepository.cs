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
                    Username = "acc-test-one@gmail.com",
                    Password = "$2b$10$2d0SUdDlNUAf8PYaEk3HduN.7Njy/jB8avpjBWgveQHT2CUbi6L9G",
                    Role = Homework.Enigmatry.Shop.Application.Constants.Constants.CustomerRole
                },
                new Customer()
                {
                    Id = 2,     
                    Username = "acc-test-two@gmail.com",
                    Password = "$2b$10$2d0SUdDlNUAf8PYaEk3HduN.7Njy/jB8avpjBWgveQHT2CUbi6L9G",
                    Role = Homework.Enigmatry.Shop.Application.Constants.Constants.CustomerRole
                },
                new Customer()
                {Id = 3,
                    Username = "acc-test-three@gmail.com",
                    Password = "$2b$10$2d0SUdDlNUAf8PYaEk3HduN.7Njy/jB8avpjBWgveQHT2CUbi6L9G",
                    Role = Homework.Enigmatry.Shop.Application.Constants.Constants.AdminRole}
            };

            var mockRepository = new Mock<ICustomerRepository>();

            mockRepository.Setup(r => r.GetAll())
                .ReturnsAsync(customers);

            mockRepository.Setup(r => r.Add(It.IsAny<Customer>()))
                .ReturnsAsync((Customer customer) =>
                {
                    customers.Add(customer);
                    return customer;
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

            mockRepository.Setup(r => r.GetByUsername(It.IsAny<string>(),It.IsAny<CancellationToken>()))
                .ReturnsAsync((string username,CancellationToken cancellationToken) =>
                {
                    return customers.FirstOrDefault(x => x.Username == username);
                });


            return mockRepository;
        }
    }
}
