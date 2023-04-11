using System.Globalization;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Logging.Shared.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Homework.Enigmatry.Shop.Application.Constants;
using Homework.Enigmatry.Shop.Application.DTOs;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Orders.Requests;
using Homework.Enigmatry.Shop.Application.Features.Orders.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Orders.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Shop.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = Constants.AdminRole)]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly LogTraceData _logTraceData;

        public OrderController(IMediator mediator, LogTraceData logTraceData)
        {
            _mediator = mediator;
            _logTraceData = logTraceData;
        }
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArticleDto>> Delete(int id, CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == Constants.UserIdLabel))
            {
                if (!int.TryParse(HttpContext.User.FindFirst(Constants.UserIdLabel)!.Value, NumberStyles.None,
                        CultureInfo.InvariantCulture, out int customerId))
                {
                    _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2},unauthorized)",
                        nameof(OrderController), nameof(Delete), id));

                    return Unauthorized();
                }
                _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{2},customerId)",
                    nameof(OrderController), nameof(Delete), id, customerId));

                var deleteOrderOperationResult = await _mediator.Send(new DeleteOrderRequest()
                    { OrderId = id, CustomerId = customerId }, cancellationToken);

                return deleteOrderOperationResult.Status switch
                {
                    OperationStatus.Success => Ok(deleteOrderOperationResult.Result),
                    OperationStatus.InvalidValues => BadRequest(deleteOrderOperationResult.ErrorMessage),
                    OperationStatus.NotExist => NotFound($"Order with id: {id} not exist"),
                    _ => throw new UnclearOperationsResultException("")
                };
            }

            return Unauthorized();
           
        }
    }
}
