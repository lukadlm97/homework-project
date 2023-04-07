using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using MediatR;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries
{
    public class GetArticleOfferByIdRequest : IRequest<OperationResult<ArticleDto>>
    {
        public int Id { get; set; }
    }
}
