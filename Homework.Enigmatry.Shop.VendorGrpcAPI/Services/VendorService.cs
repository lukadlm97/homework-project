using Grpc.Core;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.VendorGrpcAPI;
using Homework.Enigmatry.Vendor.Application.Features.Requests.Queries;
using MediatR;
using OperationStatus = Homework.Enigmatry.Shop.Domain.Enums.OperationStatus;

namespace Homework.Enigmatry.Vendor.GrpcAPI.Services
{
    public class VendorService:Shop.VendorGrpcAPI.Vendor.VendorBase
    {
        private readonly IMediator _mediator;

        public VendorService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<ArticleReply> GetArticle(ArticleRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetArticleByIdRequest() { Id = request.Id },context.CancellationToken);

            return result.Status switch
            {
                OperationStatus.Success => new ArticleReply()
                {
                    Id = result.Result.Id,
                    Price = (float)result.Result.Price,
                    Name = result.Result.Name,
                    Status = Shop.VendorGrpcAPI.OperationStatus.Success
                },
                OperationStatus.NotExist => new ArticleReply()
                {

                    Status = Shop.VendorGrpcAPI.OperationStatus.NotFound
                }
            };
        }

        public override async Task<ArticleExistReply> IsArticleExist(ArticleRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new IsVendorArticleExistRequest() { Id = request.Id },
                context.CancellationToken);

            return result ? 
                new ArticleExistReply() { Status = Shop.VendorGrpcAPI.OperationStatus.Success }
                : new ArticleExistReply() { Status = Shop.VendorGrpcAPI.OperationStatus.NotFound };

        }
    }
}
