using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
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
        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET:
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDetailsDto>> Get(int id)
        {
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
            var isArticleExist = await _mediator.Send(new IsVendorArticleExistRequest() { Id = id });

            return isArticleExist ? Ok() : NotFound();
        }
    }
}
