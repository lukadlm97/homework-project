using System.Runtime.Caching;
using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Extensions;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Application.Models;
using Homework.Enigmatry.Shop.Domain.Entities;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries
{
    public class GetArticleOfferByIdHandler : IRequestHandler<GetArticleOfferByIdRequest, OperationResult<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IVendorService _vendorService;
        private readonly MemoryCache _memoryCache;
        private readonly CacheSettings _cacheItemPolicy;
        private readonly IOrderRepository _orderRepository;

        public GetArticleOfferByIdHandler(IArticleRepository articleRepository,IVendorService vendorService,
            IMapper mapper,MemoryCache memoryCache,IOptions<CacheSettings> cacheItemPolicySettings,IOrderRepository orderRepository)
        {
            _articleRepository = articleRepository;
            _vendorService = vendorService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _cacheItemPolicy = cacheItemPolicySettings.Value;
            _orderRepository= orderRepository;
        }
        public async Task<OperationResult<ArticleDto>> Handle(GetArticleOfferByIdRequest request, CancellationToken cancellationToken)
        {
            var key =request.Id.CreateArticleCacheKey();
            var article = (Article) _memoryCache.Get(key);
            if (article != null)
            {
                return new OperationResult<ArticleDto>(OperationStatus.Success,_mapper.Map<ArticleDto>(article));
            }

            CacheItem? itemForCaching = null;
            var cacheItemPolicy = CreateCacheItemPolicy();

            var inventoryArticle = await _articleRepository.Get(request.Id);

            if (inventoryArticle != null)
            {
                if (await _orderRepository.ExistForArticle(request.Id, cancellationToken))
                {
                    return new OperationResult<ArticleDto>(OperationStatus.ArticleSold);
                }
                itemForCaching = new CacheItem(key, inventoryArticle);
                _memoryCache.Set(itemForCaching, cacheItemPolicy);

                return new OperationResult<ArticleDto>(OperationStatus.Success, _mapper.Map<ArticleDto>(inventoryArticle));
            }


            var availableArticlesFromVendors = await _vendorService.GetAvailableArticles(request.Id, cancellationToken);
            if (availableArticlesFromVendors.IsNullOrEmpty())
            {
                return new OperationResult<ArticleDto>(OperationStatus.NotExist);
            }

            var bestVendorOffer = availableArticlesFromVendors.MinBy(x => x.Price);
            var articleFromVendor = _mapper.Map<Article>(bestVendorOffer);
            
            itemForCaching = new CacheItem(key, articleFromVendor);
            _memoryCache.Set(itemForCaching, cacheItemPolicy);

            var articleDto = _mapper.Map<ArticleDto>(articleFromVendor);
            return new OperationResult<ArticleDto>(OperationStatus.Success,articleDto);
        }

        CacheItemPolicy CreateCacheItemPolicy()
        {
            if (_cacheItemPolicy.SlidingExpiration)
            {
                return new CacheItemPolicy()
                {
                    SlidingExpiration = _cacheItemPolicy.SlidingExpirationTime
                };
            }

            return new CacheItemPolicy()
            {
                //https://alastaircrabtree.com/absolute-cache-expiry-corrupts-absolutely/
                AbsoluteExpiration = DateTimeOffset.UtcNow.Add(_cacheItemPolicy.AbsoluteExpirationTime)
            };
        }
    }
}
