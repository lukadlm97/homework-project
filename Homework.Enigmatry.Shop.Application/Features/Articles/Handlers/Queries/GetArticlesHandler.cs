using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Application.Features.Articles.Validators.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries
{
    public class GetArticlesHandler:IRequestHandler<GetArticlesRequest, OperationResult<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly LogTraceData _logTraceData;

        public GetArticlesHandler(IArticleRepository articleRepository,IMapper mapper,LogTraceData logTraceData)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _logTraceData = logTraceData;
        }
        public async Task<OperationResult<ArticleDto>> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(GetArticlesHandler), nameof(Handle)));

            var validator = new GetArticlesValidator();
            var validationResult =  await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new OperationResult<ArticleDto>(OperationStatus.InvalidValues,
                    ErrorMessage: string.Join(',', validationResult.Errors
                                                                    .Select(x => x.ErrorMessage)));
            }

            var articles = await _articleRepository.GetAll();
            if (articles.IsNullOrEmpty())
            {
                return new OperationResult<ArticleDto>(OperationStatus.NotFound);
            }
            var filteredArticles = articles.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                filteredArticles = articles.Where(x => x.Name.Contains(request.Filter));
            }

            return new OperationResult<ArticleDto>(OperationStatus.Success,Results:
                _mapper.Map<IReadOnlyList<ArticleDto>>(filteredArticles
                    .Skip((request.PageNumber - 1)* request.PageSize)
                    .Take(request.PageSize)));
        }
    }
}
