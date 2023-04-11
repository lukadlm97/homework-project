using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.UnitTests.Mocks;
using Moq;
using FluentAssertions;
using Homework.Enigmatry.Shop.Application.Features.Customer.Handlers.Queries;
using Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Features.Customer
{
    public class LoginTest
    {
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IArticleRepository> _articleRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITokenService> _tokenService;

        public LoginTest()
        {
            _customerRepository = MockCustomerRepository.GetCustomerRepositoryMock();
            _articleRepository = MockArticleRepository.GetArticleRepository();
            _orderRepository = MockOrderRepository.GetOrderRepositoryMock();
            _tokenService = MockTokenService.GetMockTokeService();
            _unitOfWork = MockUnitOfWork.GetUnitOfWork(_articleRepository.Object, _customerRepository.Object, _orderRepository.Object);

        }

        [Xunit.Theory]
        [InlineData("asdfqwer", "Password123.")]
        public async Task Login_ValidUsernameAndPassword_ReturnCustomerNotExist(string username, string password)
        {
            var loginRequest = new LoginRequest() { Username = username, Password = password };
            var handler = new LoginHandler(_customerRepository.Object, _tokenService.Object, new LogTraceData());
            var result = await handler.Handle(loginRequest, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.CustomerNotExist);
        }

        [Xunit.Theory]
        [InlineData("acc-test-one@gmail.com", "Password123.")]
        public async Task Login_ValidUsernameAndPassword_ReturnSuccess(string username, string password)
        {
            var loginRequest = new LoginRequest() { Username = username, Password = password };
            var handler = new LoginHandler(_customerRepository.Object, _tokenService.Object, new LogTraceData());
            var result = await handler.Handle(loginRequest, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
        }
        [Xunit.Theory]
        [InlineData("acc-test-one@gmail.com", "Password123.-")]
        public async Task Login_ValidUsernameAndPassword_ReturnInvalidPassword(string username, string password)
        {
            var loginRequest = new LoginRequest() { Username = username, Password = password };
            var handler = new LoginHandler(_customerRepository.Object, _tokenService.Object, new LogTraceData());
            var result = await handler.Handle(loginRequest, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.InvalidPassword);
        }
    }
}
