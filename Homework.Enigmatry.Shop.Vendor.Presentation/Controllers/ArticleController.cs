using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Domain.Enums;
using Homework.Enigmatry.Vendor.Application.Features.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Enigmatry.Shop.Vendor.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LogTraceData _logTraceData;

        public ArticleController(IMediator mediator, LogTraceData logTraceData)
        {
            _mediator = mediator;
            _logTraceData = logTraceData;
        }
        // GET:
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDetailsDto>> Get(int id)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2})", nameof(ArticleController), nameof(Get), id));

            var articleOperationResult = await _mediator.Send(new GetArticleByIdRequest() { Id = id });
            return articleOperationResult.Status switch
            {
                OperationStatus.Success => Ok(articleOperationResult.Result),
                OperationStatus.InvalidValues => BadRequest(),
                OperationStatus.NotExist or OperationStatus.NotFound => NotFound($"Article with id: {id} not exist"),
                _ => throw new UnclearOperationsResultException("")
            };
        }
        [HttpGet("{id}/exist")]
        public async Task<ActionResult<ArticleDetailsDto>> Exist(int id)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2})", nameof(ArticleController), nameof(Exist), id));

            var isArticleExist = await _mediator.Send(new IsVendorArticleExistRequest() { Id = id });
            return isArticleExist ?
                Ok() : NotFound();
        }
    }
}
