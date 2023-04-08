using System.Globalization;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Application.Constants;
using Homework.Enigmatry.Shop.Application.DTOs.Article;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Enigmatry.Shop.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
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

        [Authorize(Roles = Constants.ADMIN_ROLE)]
        [HttpGet("")]
        public async Task<ActionResult<ArticleDto>> Get([FromQuery]ArticlePagingDto articlePagingDto, CancellationToken cancellationToken = default)
        {
            var articleOperationResult = await _mediator.Send(new GetArticlesRequest()
            {
                Filter = articlePagingDto.Filter,
                PageSize = articlePagingDto.PageSize,
                PageNumber = articlePagingDto.PageNumber
            }, cancellationToken);

            return articleOperationResult.Status switch
            {
                OperationStatus.Success => Ok(articleOperationResult.Results),
                OperationStatus.InvalidValues => BadRequest(),
                OperationStatus.NotExist or OperationStatus.NotFound => NotFound(),
                _ => throw new UnclearOperationsResultException("")
            };
        }

        [Authorize(Roles = Constants.CUSTOMER_ROLE)]
        [HttpGet("{id}/offers")]
        public async Task<ActionResult<ArticleDto>> GetOffers(int id, [FromQuery]ArticleRequestDto articleRequestDto,CancellationToken cancellationToken=default)
        {
            var articleOperationResult = await _mediator.Send(new GetArticleOfferByIdRequest() { Id = id,
                MaxPriceLimit = articleRequestDto.MaxArticlePrice }, cancellationToken);

            return articleOperationResult.Status switch
            {
                OperationStatus.Success => Ok(articleOperationResult.Result),
                OperationStatus.InvalidValues  => BadRequest(articleOperationResult.ErrorMessage),
                OperationStatus.NotExist or OperationStatus.NotFound => NotFound($"Article with id: {id} not exist"),
                OperationStatus.ArticleSold  => NotFound($"Article with id: {id} is sold"),
                OperationStatus.PriceGreaterThanLimit  => NotFound($"Article with id: {id} don\'t fit at price limit"),
                _ => throw new UnclearOperationsResultException("")
            };
        }

        [Authorize(Roles = Constants.CUSTOMER_ROLE)]
        [HttpGet("{id}/buy")]
        public async Task<ActionResult<ArticleDto>> Buy(int id,CancellationToken cancellationToken=default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == Constants.USER_ID_LABEL))
            {
                if (!int.TryParse(HttpContext.User.FindFirst(Constants.USER_ID_LABEL)!.Value, NumberStyles.None,
                        CultureInfo.InvariantCulture, out int customerId))
                {
                    return Unauthorized();
                }

                var articleOperationResult = await _mediator.Send(new BuyArticleRequest() { ArticleId = id,
                    CustomerId = customerId }, cancellationToken);

                return articleOperationResult.Status switch
                {
                    OperationStatus.Success => Ok(articleOperationResult.Result),
                    OperationStatus.InvalidValues => BadRequest(),
                    OperationStatus.NotExist or OperationStatus.NotFound => NotFound($"Article with id: {id} not exist"),
                    OperationStatus.ArticleSold => BadRequest($"Article with id: {id} is sold"),
                    _ => throw new UnclearOperationsResultException("")
                };
            }

            return Unauthorized();
        }

    }
}
