using AutoMapper;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Article;
using Homework.Enigmatry.Shop.Application.DTOs.Common;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries
{
    public class ArticleDetailsRequestHandler : IRequestHandler<ArticleDetailsRequest, OperationResult<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IVendorService _vendorService;

        public ArticleDetailsRequestHandler(IArticleRepository articleRepository,IVendorService vendorService, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _vendorService = vendorService;
            _mapper = mapper;
        }
        public async Task<OperationResult<ArticleDto>> Handle(ArticleDetailsRequest request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.Get(request.Id);
            var articles = await _vendorService.Get(request.Id, cancellationToken);
            if (article == null || articles.IsNullOrEmpty())
            {
                return new OperationResult<ArticleDto>(default, default, OperationStatus.NotExist);
            }
            var articleDto = _mapper.Map<ArticleDto>(article);
            return new OperationResult<ArticleDto>(articleDto, default, OperationStatus.Success);
        }
    }
}
