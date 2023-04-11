using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using MediatR;

namespace Homework.Enigmatry.Shop.Application.Features.Orders.Requests
{
    public class GetOrdersRequest : IRequest<OperationResult<OrderDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
    }
}
