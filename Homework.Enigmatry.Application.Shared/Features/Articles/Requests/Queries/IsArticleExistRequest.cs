using MediatR;

namespace Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries
{
    public class IsArticleExistRequest : IRequest<bool>
    {
        public int Id { get; set; }

    }
}
