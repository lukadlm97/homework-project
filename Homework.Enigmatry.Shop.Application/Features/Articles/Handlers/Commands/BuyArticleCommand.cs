using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.Contracts;
using MediatR;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
using Homework.Enigmatry.Shop.Domain.Entities;
using Homework.Enigmatry.Shop.Domain.Enums;
namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Commands
{
    public class BuyArticleCommand : IRequestHandler<BuyArticleRequest, OperationResult<OrderDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public BuyArticleCommand(IArticleRepository articleRepository, ICustomerRepository customerRepository, IOrderRepository orderRepository,IMapper mapper)
        {
            _articleRepository = articleRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OperationResult<OrderDto>> Handle(BuyArticleRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.Get(request.CustomerId);
            var article = await  _articleRepository.Get(request.ArticleId);
            if (customer == null || article == null)
            {
                return new OperationResult<OrderDto>(OperationStatus.NotExist);
            }

            var order = new Order()
            {
                ArticleId = article.Id,
                Article = article,
                Customer = customer,
                CustomerId = customer.Id,
                Date = DateTime.UtcNow,
                Price = article.Price,
            };

            var createdOrder= await _orderRepository.Add(order);

            return new OperationResult<OrderDto>(OperationStatus.Success, _mapper.Map<OrderDto>(createdOrder));
        }
    }
}
