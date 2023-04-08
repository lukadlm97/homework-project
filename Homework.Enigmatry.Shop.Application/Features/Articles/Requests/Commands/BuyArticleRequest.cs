using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using MediatR;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands
{
    public class BuyArticleRequest:IRequest<OperationResult<OrderDto>>
    {
        public int ArticleId { get; set; }
        public int CustomerId { get; set; }
    }
}
