using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Infrastructure.Factories.Contract;
using Homework.Enigmatry.Shop.VendorGrpcAPI;
using Microsoft.Extensions.Logging;

namespace Homework.Enigmatry.Shop.Infrastructure.Services.Vendor
{
    public class VendorGrpcRepository:IVendorGrpcRepository
    {
        private readonly IVendorGrpcFactory _vendorGrpcFactory;
        private readonly ILogger<VendorGrpcRepository> _logger;

        public VendorGrpcRepository(ILogger<VendorGrpcRepository> logger, IVendorGrpcFactory vendorGrpcFactory)
        {
            _logger = logger;
            _vendorGrpcFactory = vendorGrpcFactory;
        }
        public async Task<ArticleDetailsDto?> GetArticle(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _vendorGrpcFactory.GetVendorClient();
                var articleReply = await grpcClient.GetArticleAsync(new ArticleRequest() { Id = id },
                    cancellationToken: cancellationToken);

                return articleReply.Status switch
                {
                    OperationStatus.Success => 
                        new ArticleDetailsDto(articleReply.Id, articleReply.Name, (decimal)articleReply.Price),
                    OperationStatus.NotFound => null
                };
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex.Message,ex);
                }
                return null;
            }
        }

        public async Task<bool> IsArticleExist(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _vendorGrpcFactory.GetVendorClient();
                var articleReply = await grpcClient.IsArticleExistAsync(new ArticleRequest() { Id = id },
                    cancellationToken: cancellationToken);

                return articleReply.Status == OperationStatus.Success;
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex.Message, ex);
                }
                return false;
            }
        }
    }
}
