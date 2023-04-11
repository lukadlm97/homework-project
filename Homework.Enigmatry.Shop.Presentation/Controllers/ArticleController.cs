using System.Globalization;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Constants;
using Homework.Enigmatry.Shop.Application.DTOs;
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
        private readonly LogTraceData _logTraceData;

        public ArticleController(IMediator mediator,LogTraceData logTraceData)
        {
            _mediator = mediator;
            _logTraceData = logTraceData;
        }

        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> Get(int id, CancellationToken cancellationToken = default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2})",nameof(ArticleController),nameof(Get),id));

            var articleOperationResult = await _mediator.Send(new GetArticleByIdRequest() { Id = id }, cancellationToken);
            return articleOperationResult.Status switch
            {
                OperationStatus.Success => Ok(articleOperationResult.Result),
                OperationStatus.InvalidValues => BadRequest(),
                OperationStatus.NotExist => NotFound($"Article with id: {id} not exist"),
                _ => throw new UnclearOperationsResultException("")
            };
        }

        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("local-inventory")]
        public async Task<ActionResult<ArticleDto>> Get([FromQuery]PagingRequestDto articlePagingDto, CancellationToken cancellationToken = default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (params=size:{2},number:{3},filter:{4})", 
                nameof(ArticleController), nameof(Get), articlePagingDto.PageSize,articlePagingDto.PageNumber,articlePagingDto.Filter));

            var articleOperationResult = await _mediator.Send(new GetArticlesRequest()
            {
                Filter = articlePagingDto.Filter,
                PageSize = articlePagingDto.PageSize,
                PageNumber = articlePagingDto.PageNumber
            }, cancellationToken);

            return articleOperationResult.Status switch
            {
                OperationStatus.Success => Ok(new {Articles = articleOperationResult.Results,TotalAvailableArticles=articleOperationResult.TotalAvailable}),
                OperationStatus.InvalidValues => BadRequest(articleOperationResult.ErrorMessage),
                OperationStatus.NotExist => NotFound(),
                _ => throw new UnclearOperationsResultException("")
            };
        }

        [Authorize(Roles = Constants.CustomerRole)]
        [HttpGet("{id}/offers")]
        public async Task<ActionResult<ArticleDto>> GetOffers(int id, [FromQuery]ArticleRequestDto articleRequestDto,CancellationToken cancellationToken=default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2},maxPrice:{3})",
                nameof(ArticleController), nameof(GetOffers), id, articleRequestDto.MaxArticlePrice));

            var articleOperationResult = await _mediator.Send(new GetArticleOfferByIdRequest() { Id = id,
                MaxPriceLimit = articleRequestDto.MaxArticlePrice }, cancellationToken);
            return articleOperationResult.Status switch
            {
                OperationStatus.Success => Ok(articleOperationResult.Result),
                OperationStatus.InvalidValues  => BadRequest(articleOperationResult.ErrorMessage),
                OperationStatus.NotExist => NotFound($"Article with id: {id} not exist"),
                OperationStatus.ArticleSold  => StatusCode(302, $"Article with id: {id} is sold"),
                OperationStatus.PriceGreaterThanLimit  => StatusCode(302,$"Article with id: {id} don\'t fit at price limit"),
                _ => throw new UnclearOperationsResultException("")
            };
        }

        [Authorize(Roles = Constants.CustomerRole)]
        [HttpGet("{id}/buy")]
        public async Task<ActionResult<ArticleDto>> Buy(int id,CancellationToken cancellationToken=default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == Constants.UserIdLabel))
            {
                if (!int.TryParse(HttpContext.User.FindFirst(Constants.UserIdLabel)!.Value, NumberStyles.None,
                        CultureInfo.InvariantCulture, out int customerId))
                {
                    _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2},unauthorized)",
                        nameof(ArticleController), nameof(Buy), id));

                    return Unauthorized();
                }
                _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2},customerId)",
                    nameof(ArticleController), nameof(Buy), id, customerId));

                var articleOperationResult = await _mediator.Send(new BuyArticleRequest() 
                { ArticleId = id, CustomerId = customerId }, cancellationToken);

                return articleOperationResult.Status switch
                {
                    OperationStatus.Success => Ok(articleOperationResult.Result),
                    OperationStatus.InvalidValues => BadRequest(articleOperationResult.ErrorMessage),
                    OperationStatus.NotExist => NotFound($"Article with id: {id} not exist"),
                    OperationStatus.ArticleSold => BadRequest($"Article with id: {id} is sold"),
                    _ => throw new UnclearOperationsResultException("")
                };
            }

            return Unauthorized();
        }

    }
}
