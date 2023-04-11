using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Logging.Shared.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Homework.Enigmatry.Shop.Application.Constants;
using Homework.Enigmatry.Shop.Application.DTOs;
using Homework.Enigmatry.Shop.Application.Features.Orders.Requests;
using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Shop.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LogTraceData _logTraceData;

        public OrderController(IMediator mediator, LogTraceData logTraceData)
        {
            _mediator = mediator;
            _logTraceData = logTraceData;
        }
        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("")]
        public async Task<ActionResult<ArticleDto>> Get([FromQuery] PagingRequestDto orderPagingDto, CancellationToken cancellationToken = default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (params=size:{2},number:{3},filter:{4})",
                nameof(OrderController), nameof(Get), orderPagingDto.PageSize, orderPagingDto.PageNumber, orderPagingDto.Filter));

            var orderOperationResult = await _mediator.Send(new GetOrdersRequest()
            {
                Filter = orderPagingDto.Filter,
                PageSize = orderPagingDto.PageSize,
                PageNumber = orderPagingDto.PageNumber
            }, cancellationToken);
            return orderOperationResult.Status switch
            {
                OperationStatus.Success => Ok(new { Orders = orderOperationResult.Results,TotalAvailableOrders=orderOperationResult.TotalAvailable}),
                OperationStatus.InvalidValues => BadRequest(orderOperationResult.ErrorMessage),
                OperationStatus.NotExist => NotFound(),
                _ => throw new UnclearOperationsResultException("")
            };
        }
    }
}
