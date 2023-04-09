using MediatR;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Application.Shared.Features.Articles.Handlers.Queries;
using Homework.Enigmatry.Logging.Shared.Contracts;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries
{
    public class IsArticleExistHandler : IRequestHandler<IsArticleExistRequest,bool>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly LogTraceData _logTraceData;

        public IsArticleExistHandler(IArticleRepository articleRepository,LogTraceData logTraceData)
        {
            _articleRepository = articleRepository;
            _logTraceData = logTraceData;
        }

        public async Task<bool> Handle(IsArticleExistRequest request, CancellationToken cancellationToken)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(GetArticleByIdHandler), nameof(Handle)));

            return await _articleRepository.Exists(request.Id);
        }
    }
}
