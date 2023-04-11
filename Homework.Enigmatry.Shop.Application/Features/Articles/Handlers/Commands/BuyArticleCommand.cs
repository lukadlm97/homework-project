using System.Runtime.Caching;
using AutoMapper;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Application.Shared.Models;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;
using MediatR;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using Homework.Enigmatry.Shop.Application.Extensions;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Articles.Validators.Commands;
using Homework.Enigmatry.Shop.Domain.Entities;
using Homework.Enigmatry.Shop.Domain.Enums;
using Microsoft.Extensions.Options;
using Homework.Enigmatry.Shop.Application.Exceptions;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Commands
{
    public class BuyArticleCommand : IRequestHandler<BuyArticleRequest, OperationResult<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly MemoryCache _memoryCache;
        private readonly LogTraceData _logTraceData;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PersistenceSettings _peristanceSettings;

        public BuyArticleCommand(MemoryCache memoryCache, IUnitOfWork unitOfWork,
            IMapper mapper,LogTraceData logTraceData,IOptions<PersistenceSettings> options)
        {
            _memoryCache = memoryCache;
            _peristanceSettings = options.Value;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logTraceData = logTraceData;
        }

        public async Task<OperationResult<OrderDto>> Handle(BuyArticleRequest request, CancellationToken cancellationToken)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(BuyArticleCommand), nameof(Handle)));

            var validator = new BuyArticleValidator(_unitOfWork.CustomerRepository, _memoryCache);
            var validatorResult = await validator.ValidateAsync(request,cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new OperationResult<OrderDto>(OperationStatus.InvalidValues,
                    ErrorMessage: string.Join(',', validatorResult.Errors.Select(x => x.ErrorMessage)));
            }
            
            if (await _unitOfWork.OrderRepository.ExistForArticle(request.ArticleId, cancellationToken))
            {
                return new OperationResult<OrderDto>(OperationStatus.ArticleSold);
            }

            var customer = await _unitOfWork.CustomerRepository.Get(request.CustomerId);
            var key = request.ArticleId.CreateArticleCacheKey();
            var itemFromCache = _memoryCache.Get(key);

            Article article = (Article)itemFromCache;
            if (!_peristanceSettings.UseInMemory)
            {
                article = await _unitOfWork.ArticleRepository.Get(request.ArticleId);
                if (article == null)
                {
                    throw new UnavailableAtLocalPersistenceStorageException("Its not constructed to support saving of orders with articles (on disc) which are not at our system!!!");
                }
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

            var createdOrder= await _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.Save();

            _memoryCache.Remove(article.Id.CreateArticleCacheKey());

            return new OperationResult<OrderDto>(OperationStatus.Success, 
                new OrderDto(
                    order.Id,
                    new ArticleDto(order.ArticleId,order.Article.Name,order.Article.Price),
                    order.Price,
                    new CustomerDto(order.CustomerId,order.Customer.Username))
                );
        }
    }
}
