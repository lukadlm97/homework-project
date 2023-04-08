using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
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
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ArticleDto>> Login([FromBody] AuthRequestDto requestDto,CancellationToken cancellationToken=default)
        {
            var logInOperationResult = await _mediator.Send(new LoginRequest()
            {
                Username =requestDto.Username,
                Password = requestDto.Password
            },cancellationToken);

            return logInOperationResult.Status switch
            {
                OperationStatus.Success => Ok(logInOperationResult.Result),
                OperationStatus.NotExist or OperationStatus.NotFound => NotFound(),
                _ => throw new UnclearOperationsResultException("")
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<ArticleDto>> Register([FromBody] AuthRequestDto requestDto, CancellationToken cancellationToken = default)
        {
            var registerOperationResult = await _mediator.Send(new RegisterRequest()
            {
                Username = requestDto.Username,
                Password = requestDto.Password
            }, cancellationToken);

            return registerOperationResult.Status switch
            {
                OperationStatus.Success => Ok(registerOperationResult.Result),
                OperationStatus.CustomerExist => BadRequest(),
                _ => throw new UnclearOperationsResultException("")
            };
        }
    }
}
