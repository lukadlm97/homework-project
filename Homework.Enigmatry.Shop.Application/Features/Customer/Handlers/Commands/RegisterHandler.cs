using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;
using Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Commands;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;

namespace Homework.Enigmatry.Shop.Application.Features.Customer.Handlers.Commands
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, OperationResult<AuthDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;

        public RegisterHandler(ICustomerRepository customerRepository,ITokenService tokenService)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
        }
        public async Task<OperationResult<AuthDto>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.GetByUsername(request.Username, cancellationToken);
            if (existingCustomer != null)
            {
                return new OperationResult<AuthDto>(OperationStatus.CustomerExist);
            }

            var customer = new Domain.Entities.Customer()
            {
                Username = request.Username,
                Password = GenerateHash(request.Password),
                Role = Constants.Constants.CUSTOMER_ROLE
            };

            existingCustomer =await _customerRepository.Add(customer);

            var authDto = new AuthDto(_tokenService.CreateToken(customer, customer.Role),
                existingCustomer.Username);
            return new OperationResult<AuthDto>(OperationStatus.Success, authDto);
        }
      

        private string GenerateHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

       
    }
}
