using MediatR;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries
{
    public class IsArticleExistHandler : IRequestHandler<IsArticleExistRequest,bool>
    {
        private readonly IArticleRepository _articleRepository;

        public IsArticleExistHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<bool> Handle(IsArticleExistRequest request, CancellationToken cancellationToken)
        {
            return await _articleRepository.Exists(request.Id);
        }
    }
}
