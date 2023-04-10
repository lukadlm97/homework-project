using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;
using Homework.Enigmatry.Shop.Application.Features.Articles.Validators.Commands;
using Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Commands;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
using System.Runtime.Caching;
using Homework.Enigmatry.Shop.Application.Features.Customer.Validators.Commands;
using Homework.Enigmatry.Shop.Application.DTOs.Order;

namespace Homework.Enigmatry.Shop.Application.Features.Customer.Handlers.Commands
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, OperationResult<AuthDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly LogTraceData _logTraceData
            ;

        public RegisterHandler(IUnitOfWork unitOfWork, ITokenService tokenService,LogTraceData  logTraceData)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _logTraceData = logTraceData;
        }
        public async Task<OperationResult<AuthDto>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(RegisterHandler), nameof(Handle)));

            var validator = new RegisterValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);


            if (!validatorResult.IsValid)
            {
                return new OperationResult<AuthDto>(OperationStatus.InvalidValues,
                    ErrorMessage: string.Join(',', validatorResult.Errors.Select(x => x.ErrorMessage)));
            }

            var existingCustomer = await _unitOfWork.CustomerRepository.GetByUsername(request.Username.ToLower(), cancellationToken);
            if (existingCustomer != null)
            {
                return new OperationResult<AuthDto>(OperationStatus.CustomerExist);
            }

            var customer = new Domain.Entities.Customer()
            {
                Username = request.Username,
                Password = GenerateHash(request.Password),
                Role = Constants.Constants.CustomerRole
            };

            existingCustomer =await _unitOfWork.CustomerRepository.Add(customer);
            await _unitOfWork.Save();
            

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
