using AutoMapper;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Article;
using Homework.Enigmatry.Shop.Application.DTOs.Common;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;
using MediatR;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries
{
    public class ArticleDetailsRequestHandler : IRequestHandler<ArticleDetailsRequest, OperationResult<ArticleDto>>
    {
        private readonly IArticleRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public ArticleDetailsRequestHandler(IArticleRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult<ArticleDto>> Handle(ArticleDetailsRequest request, CancellationToken cancellationToken)
        {
            var article = await _leaveAllocationRepository.Get(request.Id);
            if (article == null)
            {
                return new OperationResult<ArticleDto>(default, default, OperationStatus.NotExist);
            }
            var articleDto = _mapper.Map<ArticleDto>(article);
            return new OperationResult<ArticleDto>(articleDto, default, OperationStatus.Success);
        }
    }
}
