using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;
using Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;

namespace Homework.Enigmatry.Shop.Application.Features.Customer.Handlers.Queries
{
    public class LoginHandler : IRequestHandler<LoginRequest, OperationResult<AuthDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;

        public LoginHandler(ICustomerRepository customerRepository,ITokenService tokenService)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
        }
        public async Task<OperationResult<AuthDto>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByUsername(request.Username, cancellationToken);
            if (customer == null)
            {
                return new OperationResult<AuthDto>(OperationStatus.NotExist);
            }

            if (!CheckPassword(request.Password, customer.Password))
            {
                return new OperationResult<AuthDto>(OperationStatus.InvalidPassword);
            }
            
            var authDto = new AuthDto(_tokenService.CreateToken(customer, customer.Role),
                customer.Username);
            return new OperationResult<AuthDto>(OperationStatus.Success, authDto);
        }

        private bool CheckPassword(string password, string hashedPassword)
        {
           return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
