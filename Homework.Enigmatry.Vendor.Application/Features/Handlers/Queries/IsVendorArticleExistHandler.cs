using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Vendor.Application.Contracts;
using MediatR;
using Homework.Enigmatry.Vendor.Application.Features.Requests.Queries;

namespace Homework.Enigmatry.Vendor.Application.Features.Handlers.Queries
{
    public class IsVendorArticleExistHandler : IRequestHandler<IsVendorArticleExistRequest, bool>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ISalesAgent _salesAgent;

        public IsVendorArticleExistHandler(IArticleRepository articleRepository,ISalesAgent salesAgent)
        {
            _articleRepository = articleRepository;
            _salesAgent = salesAgent; 
        }

        public async Task<bool> Handle(IsVendorArticleExistRequest request, CancellationToken cancellationToken)
        {
            if (await _articleRepository.Exists(request.Id))
            {
                return true;
            }

            return await _salesAgent.CheckInventory(request.Id);
        }
    }
}
