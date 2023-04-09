using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Logging.Shared.Contracts;
using MediatR;
using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Application.Shared.Features.Articles.Handlers.Queries
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdRequest, OperationResult<ArticleDetailsDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly LogTraceData _logTraceData;

        public GetArticleByIdHandler(IArticleRepository articleRepository, IMapper mapper,LogTraceData logTraceData)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _logTraceData = logTraceData;
        }
        public async Task<OperationResult<ArticleDetailsDto>> Handle(GetArticleByIdRequest request, CancellationToken cancellationToken)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(GetArticleByIdHandler), nameof(Handle)));

            var inventoryArticle = await _articleRepository.Get(request.Id);
            if (inventoryArticle == null)
            {
                return new OperationResult<ArticleDetailsDto>(OperationStatus.NotFound);
            }

            return new OperationResult<ArticleDetailsDto>(OperationStatus.Success, _mapper.Map<ArticleDetailsDto>(inventoryArticle));
        }
    }
}
