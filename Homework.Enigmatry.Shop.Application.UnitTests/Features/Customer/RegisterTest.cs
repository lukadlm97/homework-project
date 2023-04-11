using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Commands;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Customer.Handlers.Commands;
using Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Commands;
using Homework.Enigmatry.Shop.Application.UnitTests.Mocks;
using Homework.Enigmatry.Shop.Domain.Enums;
using Homework.Enigmatry.Shop.Infrastructure.Services.Token;
using Moq;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Features.Customer
{
    public class RegisterTest
    {
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IArticleRepository> _articleRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITokenService> _tokenService;

        public RegisterTest()
        {
            _customerRepository = MockCustomerRepository.GetCustomerRepositoryMock();
            _articleRepository = MockArticleRepository.GetArticleRepository();
            _orderRepository = MockOrderRepository.GetOrderRepositoryMock();
            _tokenService = MockTokenService.GetMockTokeService();
            _unitOfWork = MockUnitOfWork.GetUnitOfWork(_articleRepository.Object, _customerRepository.Object, _orderRepository.Object);

        }

        [Xunit.Theory]
        [InlineData("asdfqwer", "Password123.")]
        public async Task Register_ValidUsernameAndPassword_ReturnSuccess(string username, string password)
        {
            var registerRequest = new RegisterRequest(){Username = username,Password = password};
            var handler = new RegisterHandler(_unitOfWork.Object, _tokenService.Object, new LogTraceData());
            var result = await handler.Handle(registerRequest, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
        }

        [Xunit.Theory]
        [InlineData("asdfqwer", "Password123")]
        [InlineData("asdfqwer", "password123.")]
        [InlineData("asdfqwer", "password.")]
        [InlineData("asdfqwer", "Pa1.")]
        [InlineData("a", "password123.")]
        public async Task Register_InvalidUsernameOrPassword_ReturnInvalidValues(string username, string password)
        {
            var registerRequest = new RegisterRequest() { Username = username, Password = password };
            var handler = new RegisterHandler(_unitOfWork.Object, _tokenService.Object, new LogTraceData());
            var result = await handler.Handle(registerRequest, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.InvalidValues);
        }

        [Xunit.Theory]
        [InlineData("acc-test-one@gmail.com", "Password123.")]
        public async Task Register_CustomerExist_ReturnCustomerExist(string username, string password)
        {
            var registerRequest = new RegisterRequest() { Username = username, Password = password };
            var handler = new RegisterHandler(_unitOfWork.Object, _tokenService.Object, new LogTraceData());
            var result = await handler.Handle(registerRequest, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.CustomerExist);
        }

    }
}
