using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Infrastructure.Factories.Contract;
using Homework.Enigmatry.Shop.VendorGrpcAPI;
using Microsoft.Extensions.Logging;

namespace Homework.Enigmatry.Shop.Infrastructure.Services.Vendor
{
    public class VendorGrpcRepository:IVendorGrpcRepository
    {
        private readonly IVendorGrpcFactory _vendorGrpcFactory;
        private readonly IHighPerformanceLogger _logger;
        private readonly LogTraceData _logTraceData;

        public VendorGrpcRepository(IHighPerformanceLogger logger, IVendorGrpcFactory vendorGrpcFactory,LogTraceData logTraceData)
        {
            _logger = logger;
            _vendorGrpcFactory = vendorGrpcFactory;
            _logTraceData = logTraceData;
        }
        public async Task<ArticleDetailsDto?> GetArticle(int id, CancellationToken cancellationToken = default)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1} (id:{3})", nameof(VendorGrpcRepository), nameof(GetArticle),id));
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
                _logger.Log(ex.Message,ex.InnerException,LogLevel.Error);
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
                _logger.Log(ex.Message, ex.InnerException, LogLevel.Error);
                return false;
            }
        }
    }
}
