using System.Runtime.Caching;
using AutoMapper;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using MediatR;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using Homework.Enigmatry.Shop.Application.Extensions;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Articles.Validators.Commands;
using Homework.Enigmatry.Shop.Domain.Entities;
using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Commands
{
    public class BuyArticleCommand : IRequestHandler<BuyArticleRequest, OperationResult<OrderDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly MemoryCache _memoryCache;
        private readonly LogTraceData _logTraceData;

        public BuyArticleCommand(MemoryCache memoryCache, ICustomerRepository customerRepository, IOrderRepository orderRepository,IMapper mapper,LogTraceData logTraceData)
        {
            _memoryCache = memoryCache;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logTraceData = logTraceData;
        }

        public async Task<OperationResult<OrderDto>> Handle(BuyArticleRequest request, CancellationToken cancellationToken)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(BuyArticleCommand), nameof(Handle)));

            var validator = new BuyArticleValidator(_customerRepository, _memoryCache);
            var validatorResult = await validator.ValidateAsync(request,cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new OperationResult<OrderDto>(OperationStatus.InvalidValues,
                    ErrorMessage: string.Join(',', validatorResult.Errors.Select(x => x.ErrorMessage)));
            }
            
            if (await _orderRepository.ExistForArticle(request.ArticleId, cancellationToken))
            {
                return new OperationResult<OrderDto>(OperationStatus.ArticleSold);
            }

            var customer = await _customerRepository.Get(request.CustomerId);
            var key = request.ArticleId.CreateArticleCacheKey();
            var itemFromCache = _memoryCache.Get(key);

            if (customer == null || itemFromCache == null)
            {
                return new OperationResult<OrderDto>(OperationStatus.NotFound);
            }

            Article article = (Article)itemFromCache;
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
            _memoryCache.Remove(article.Id.CreateArticleCacheKey());

            return new OperationResult<OrderDto>(OperationStatus.Success, _mapper.Map<OrderDto>(createdOrder));
        }
    }
}
