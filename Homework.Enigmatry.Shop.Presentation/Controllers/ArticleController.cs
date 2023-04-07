﻿using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
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
        // GET: api/<LeaveAllocationsController>
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> Get(int id)
        {
            var articleOperationResult = await _mediator.Send(new GetArticleOfferByIdRequest() { Id = id });

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
