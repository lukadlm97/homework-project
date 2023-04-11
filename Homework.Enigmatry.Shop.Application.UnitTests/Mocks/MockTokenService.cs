using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Moq;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Mocks
{
    public static class MockTokenService
    {
        public static Mock<ITokenService> GetMockTokeService()
        {
            var mockService = new Mock<ITokenService>();

            mockService.Setup(r => 
                    r.CreateToken(It.IsAny<Customer>(),It.IsAny<string>())).Returns((Customer Customer, string role) =>
                {
                    return "test";
                });


           
            return mockService;
        }
    }
}
