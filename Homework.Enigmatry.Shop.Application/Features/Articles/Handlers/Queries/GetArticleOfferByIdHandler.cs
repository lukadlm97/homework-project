using System.Runtime.Caching;
using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries
{
    public class GetArticleOfferByIdHandler : IRequestHandler<GetArticleOfferByIdRequest, OperationResult<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IVendorService _vendorService;
        private readonly MemoryCache _memoryCache;

        public GetArticleOfferByIdHandler(IArticleRepository articleRepository,IVendorService vendorService, IMapper mapper,MemoryCache memoryCache)
        {
            _articleRepository = articleRepository;
            _vendorService = vendorService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        public async Task<OperationResult<ArticleDto>> Handle(GetArticleOfferByIdRequest request, CancellationToken cancellationToken)
        {
            var key = nameof(ArticleDto) + request.Id;
            var article = (ArticleDto) _memoryCache.Get(key);
            if (article != null)
            {
                return new OperationResult<ArticleDto>(OperationStatus.Success,article);
            }

            var inventoryArticle = await _articleRepository.Get(request.Id);
            var articles = await _vendorService.Get(request.Id, cancellationToken);
            if (inventoryArticle != null)
            {
                articles.Add(_mapper.Map<ArticleDto>(inventoryArticle));
            }
            if (articles.IsNullOrEmpty())
            {
                return new OperationResult<ArticleDto>(OperationStatus.NotExist);
            }

            var bestDeal = articles.MinBy(x => x.Id);
            var articleDto = _mapper.Map<ArticleDto>(bestDeal);
           //TODO _memoryCache.Set(key,articleDto);
            return new OperationResult<ArticleDto>(OperationStatus.Success,articleDto);
        }
    }
}
