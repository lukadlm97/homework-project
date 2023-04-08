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

        public RegisterHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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
                Password = GenerateHash(request.Password)
            };

            existingCustomer =await _customerRepository.Add(customer);
            //TODO generate token

            return new OperationResult<AuthDto>(OperationStatus.Success, new AuthDto("token", existingCustomer.Username));
        }
      

        private string GenerateHash(string password)
        {

            return password;
        }

       
    }
}
