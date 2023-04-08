
using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using MediatR;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries
{
    public class GetArticlesRequest:IRequest<OperationResult<ArticleDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize{ get; set; }
        public string? Filter { get; set; }
    }
}
