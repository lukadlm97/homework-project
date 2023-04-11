using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Moq;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork(IArticleRepository articleRepository,
            ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.CustomerRepository).Returns(customerRepository);
            mockUnitOfWork.Setup(x => x.ArticleRepository).Returns(articleRepository);
            mockUnitOfWork.Setup(x => x.OrderRepository).Returns(orderRepository);

            return mockUnitOfWork;
        }
    }
}
