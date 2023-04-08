using AutoMapper;
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
        private readonly IMapper _mapper;

        public LoginHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
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
            //TODO generate token

            return new OperationResult<AuthDto>(OperationStatus.Success, new AuthDto("token", customer.Username));
        }

        private bool CheckPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || password != hashedPassword)
            {
                return false;
            }
            return true;
        }
    }
}
