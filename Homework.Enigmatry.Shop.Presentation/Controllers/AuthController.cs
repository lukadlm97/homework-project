using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;
using Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Enigmatry.Shop.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LogTraceData _logTraceData;

        public AuthController(IMediator mediator,LogTraceData logTraceData)
        {
            _mediator = mediator;
            _logTraceData = logTraceData;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ArticleDto>> Login([FromBody] AuthRequestDto requestDto,CancellationToken cancellationToken=default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (username:{2})", nameof(AuthController), nameof(Login), requestDto.Username));

            var logInOperationResult = await _mediator.Send(new LoginRequest()
            {
                Username =requestDto.Username,
                Password = requestDto.Password
            },cancellationToken);

            return logInOperationResult.Status switch
            {
                OperationStatus.Success => Ok(logInOperationResult.Result),
                OperationStatus.CustomerNotExist => NotFound(),
                OperationStatus.InvalidPassword => Unauthorized(),
                _ => throw new UnclearOperationsResultException("")
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<ArticleDto>> Register([FromBody] AuthRequestDto requestDto, CancellationToken cancellationToken = default)
        {

            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (username:{2},password:{3})", 
                nameof(Register), nameof(Login), requestDto.Username, requestDto.Password));

            var registerOperationResult = await _mediator.Send(new RegisterRequest()
            {
                Username = requestDto.Username,
                Password = requestDto.Password
            }, cancellationToken);

            return registerOperationResult.Status switch
            {
                OperationStatus.Success => Ok(registerOperationResult.Result),
                OperationStatus.CustomerExist => BadRequest(string.Format("customer with username:{0} exist",requestDto.Username)),
                OperationStatus.InvalidValues => BadRequest(registerOperationResult.ErrorMessage),
                _ => throw new UnclearOperationsResultException("")
            };
        }
    }
}
