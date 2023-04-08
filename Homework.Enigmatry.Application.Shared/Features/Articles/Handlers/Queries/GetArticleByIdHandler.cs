using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using MediatR;
using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Application.Shared.Features.Articles.Handlers.Queries
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdRequest, OperationResult<ArticleDetailsDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticleByIdHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult<ArticleDetailsDto>> Handle(GetArticleByIdRequest request, CancellationToken cancellationToken)
        {
            var inventoryArticle = await _articleRepository.Get(request.Id);
            if (inventoryArticle == null)
            {
                return new OperationResult<ArticleDetailsDto>(OperationStatus.NotFound);
            }

            return new OperationResult<ArticleDetailsDto>(OperationStatus.Success, _mapper.Map<ArticleDetailsDto>(inventoryArticle));
        }
    }
}
