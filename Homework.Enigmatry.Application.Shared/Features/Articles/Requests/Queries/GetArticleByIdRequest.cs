using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using MediatR;

namespace Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries
{
    public class GetArticleByIdRequest : IRequest<OperationResult<ArticleDto>>
    {
        public int Id { get; set; }

    }
}
