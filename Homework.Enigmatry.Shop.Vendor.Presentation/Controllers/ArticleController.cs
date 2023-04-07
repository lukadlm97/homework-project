﻿using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Enigmatry.Shop.Vendor.Presentation.Controllers
{
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET:
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> Get(int id)
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
        public async Task<ActionResult<ArticleDto>> Exist(int id)
        {
            var isArticleExist = await _mediator.Send(new IsArticleExistRequest() { Id = id });

            return isArticleExist ? Ok() : NotFound();
        }
    }
}