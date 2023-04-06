using MediatR;
using Homework.Enigmatry.Shop.Application.DTOs.Article;
using Homework.Enigmatry.Shop.Application.DTOs.Common;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries
{
    public class ArticleDetailsRequest : IRequest<OperationResult<ArticleDto>>
    {
        public int Id { get; set; }

    }
}
