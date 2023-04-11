using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using MediatR;

namespace Homework.Enigmatry.Shop.Application.Features.Orders.Requests.Commands
{
    public class DeleteOrderRequest : IRequest<OperationResult<OrderDto>>
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
    }
}
