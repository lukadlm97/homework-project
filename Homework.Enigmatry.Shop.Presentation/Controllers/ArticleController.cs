using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Application.Constants;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Enigmatry.Shop.Presentation.Controllers
{
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = Constants.ADMIN_ROLE)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> Get(int id, CancellationToken cancellationToken = default)
        {
            var articleOperationResult = await _mediator.Send(new GetArticleByIdRequest() { Id = id }, cancellationToken);

            return articleOperationResult.Status switch
            {
                OperationStatus.Success => Ok(articleOperationResult.Result),
                OperationStatus.InvalidValues => BadRequest(),
                OperationStatus.NotExist or OperationStatus.NotFound => NotFound($"Article with id: {id} not exist"),
                _ => throw new UnclearOperationsResultException("")
            };
        }

        [Authorize(Roles = Constants.CUSTOMER_ROLE)]
        [HttpGet("{id}/offers")]
        public async Task<ActionResult<ArticleDto>> GetOffers(int id,CancellationToken cancellationToken=default)
        {
            var articleOperationResult = await _mediator.Send(new GetArticleOfferByIdRequest() { Id = id }, cancellationToken);

            return articleOperationResult.Status switch
            {
                OperationStatus.Success => Ok(articleOperationResult.Result),
                OperationStatus.InvalidValues => BadRequest(),
                OperationStatus.NotExist or OperationStatus.NotFound => NotFound($"Article with id: {id} not exist"),
                _ => throw new UnclearOperationsResultException("")
            };
        }

        [Authorize(Roles = Constants.CUSTOMER_ROLE)]
        [HttpGet("{id}/buy")]
        public async Task<ActionResult<ArticleDto>> Buy(int id,CancellationToken cancellationToken=default)
        {
            var articleOperationResult = await _mediator.Send(new BuyArticleRequest() { ArticleId = id,CustomerId = 1}, cancellationToken);

            return articleOperationResult.Status switch
            {
                OperationStatus.Success => Ok(articleOperationResult.Result),
                OperationStatus.InvalidValues => BadRequest(),
                OperationStatus.NotExist or OperationStatus.NotFound => NotFound($"Article with id: {id} not exist"),
                _ => throw new UnclearOperationsResultException("")
            };
        }

    }
}
