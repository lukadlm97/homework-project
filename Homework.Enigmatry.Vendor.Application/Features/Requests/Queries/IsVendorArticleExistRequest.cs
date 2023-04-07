using MediatR;

namespace Homework.Enigmatry.Vendor.Application.Features.Requests.Queries
{
    public class IsVendorArticleExistRequest : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
