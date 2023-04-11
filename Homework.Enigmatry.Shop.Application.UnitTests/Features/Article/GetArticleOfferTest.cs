using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Moq;
using System.Runtime.Caching;
using FluentAssertions;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Profiles;
using Homework.Enigmatry.Shop.Application.UnitTests.Mocks;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Application.Models;
using Homework.Enigmatry.Shop.Domain.Enums;
using Homework.Enigmatry.Shop.Infrastructure.Services.Vendor;
using Microsoft.Extensions.Options;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Features.Article
{
    public class GetArticleOfferTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _articleRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IVendorProvider> _vendorRepositoryMock;
        private readonly Mock<IVendorGrpcProvider> _grpcVendor;
        private readonly MemoryCache _memoryCache;
        private const decimal MaxLimitPricePass = 2200;
        private const int ArticleIdPass = 2;

        public GetArticleOfferTest()
        {
            _articleRepositoryMock = MockArticleRepository.GetArticleRepository();
            _orderRepositoryMock = MockOrderRepository.GetOrderRepositoryMock();
            _vendorRepositoryMock = MockVendorRepository.GetVendorRepositoryMock();
            _grpcVendor = MockVendorGrpcRepository.GetVendorRepositoryMock();
            _memoryCache = MockMemoryCache.GetMemoryCache();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
                c.AddProfile<Homework.Enigmatry.Application.Shared.Profiles.MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }


        [Xunit.Theory]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        public async Task GetArticleOfferById_ArticleExistingAtCache_ReturnArticle(int id)
        {
            var vendorOptions = Options.Create(new VendorSettings());
            var vendorService = new VendorService(vendorOptions, _vendorRepositoryMock.Object,_grpcVendor.Object,new LogTraceData());

            var cacheItemSettings = Options.Create(new CacheSettings());
            var handler = new GetArticleOfferByIdHandler(_articleRepositoryMock.Object,vendorService,_mapper,_memoryCache, cacheItemSettings, _orderRepositoryMock.Object,new LogTraceData());

            var result = await handler.Handle(new GetArticleOfferByIdRequest() { Id = id,MaxPriceLimit = MaxLimitPricePass }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
            result.Result.Name.Should().Be(string.Format("{0} {1}", Constants.Constants.ArticleName, id));
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetArticleOfferById_ArticleExistingAtRepository_ReturnArticle(int id)
        {
            var vendorOptions = Options.Create(new VendorSettings());
            var vendorService = new VendorService(vendorOptions, _vendorRepositoryMock.Object, _grpcVendor.Object, new LogTraceData());

            var cacheItemSettings = Options.Create(new CacheSettings());
            var handler = new GetArticleOfferByIdHandler(_articleRepositoryMock.Object, vendorService, _mapper, _memoryCache, cacheItemSettings, _orderRepositoryMock.Object, new LogTraceData());

            var result = await handler.Handle(new GetArticleOfferByIdRequest() { Id = id, MaxPriceLimit = MaxLimitPricePass }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
            result.Result.Name.Should().Be(string.Format("{0} {1}", Constants.Constants.ArticleName, id));
        }

        [Xunit.Theory]
        [InlineData(111)]
        [InlineData(112)]
        [InlineData(113)]
        public async Task GetArticleOfferById_ArticleExistingAtVendor_ReturnArticle(int id)
        {
            var vendorOptions = Options.Create(new VendorSettings());
            var vendorService = new VendorService(vendorOptions, _vendorRepositoryMock.Object, _grpcVendor.Object, new LogTraceData());

            var cacheItemSettings = Options.Create(new CacheSettings());
            var handler = new GetArticleOfferByIdHandler(_articleRepositoryMock.Object, vendorService, _mapper, _memoryCache, cacheItemSettings, _orderRepositoryMock.Object, new LogTraceData());

            var result = await handler.Handle(new GetArticleOfferByIdRequest() { Id = id, MaxPriceLimit = MaxLimitPricePass }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
            result.Result.Name.Should().Be(string.Format("{0} {1}", Constants.Constants.ArticleName, id));
        }

        [Xunit.Theory]
        [InlineData(1111)]
        [InlineData(1112)]
        [InlineData(1113)]
        public async Task GetArticleOfferById_ArticleExistingAtVendorGrpc_ReturnArticle(int id)
        {
            var vendorOptions = Options.Create(new VendorSettings());
            var vendorService = new VendorService(vendorOptions, _vendorRepositoryMock.Object, _grpcVendor.Object, new LogTraceData());

            var cacheItemSettings = Options.Create(new CacheSettings());
            var handler = new GetArticleOfferByIdHandler(_articleRepositoryMock.Object, vendorService, _mapper, _memoryCache, cacheItemSettings, _orderRepositoryMock.Object, new LogTraceData());

            var result = await handler.Handle(new GetArticleOfferByIdRequest() { Id = id, MaxPriceLimit = MaxLimitPricePass }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
            result.Result.Name.Should().Be(string.Format("{0} {1}", Constants.Constants.ArticleName, id));
        }
        [Xunit.Theory]
        [InlineData(20)]
        [InlineData(21)]
        [InlineData(22)]
        public async Task GetArticleOfferById_ArticleNotExist_ReturnNotFoundArticle(int id)
        {
            var vendorOptions = Options.Create(new VendorSettings());
            var vendorService = new VendorService(vendorOptions, _vendorRepositoryMock.Object, _grpcVendor.Object, new LogTraceData());

            var cacheItemSettings = Options.Create(new CacheSettings());
            var handler = new GetArticleOfferByIdHandler(_articleRepositoryMock.Object, vendorService, _mapper, _memoryCache, cacheItemSettings, _orderRepositoryMock.Object, new LogTraceData());

            var result = await handler.Handle(new GetArticleOfferByIdRequest() { Id = id, MaxPriceLimit = MaxLimitPricePass }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.NotExist);
        }

        [Xunit.Theory]
        [InlineData(2200,false)]
        [InlineData(20,true)]
        public async Task GetArticleOfferById_MaxPriceLimitCheck(decimal maxPriceLimit,bool priceGreaterThanLimit)
        {
            var vendorOptions = Options.Create(new VendorSettings());
            var vendorService = new VendorService(vendorOptions, _vendorRepositoryMock.Object, _grpcVendor.Object, new LogTraceData());

            var cacheItemSettings = Options.Create(new CacheSettings());
            var handler = new GetArticleOfferByIdHandler(_articleRepositoryMock.Object, vendorService, _mapper, _memoryCache, cacheItemSettings, _orderRepositoryMock.Object, new LogTraceData());

            var result = await handler.Handle(new GetArticleOfferByIdRequest() { Id = ArticleIdPass, MaxPriceLimit = maxPriceLimit }, CancellationToken.None);
            if (priceGreaterThanLimit)
            {
                result.Status.Should().Be(OperationStatus.PriceGreaterThanLimit);
                return;
            }
            result.Status.Should().Be(OperationStatus.Success);
        }

        [Xunit.Theory]
        [InlineData(-1, true)]
        [InlineData(0, true)]
        [InlineData(2200, false)]
        public async Task GetArticleOfferById_MaxPriceValidationCheck(decimal maxPriceLimit, bool validationIssue)
        {
            var vendorOptions = Options.Create(new VendorSettings());
            var vendorService = new VendorService(vendorOptions, _vendorRepositoryMock.Object, _grpcVendor.Object, new LogTraceData());

            var cacheItemSettings = Options.Create(new CacheSettings());
            var handler = new GetArticleOfferByIdHandler(_articleRepositoryMock.Object, vendorService, _mapper, _memoryCache, cacheItemSettings, _orderRepositoryMock.Object, new LogTraceData());

            var result = await handler.Handle(new GetArticleOfferByIdRequest() { Id = ArticleIdPass, MaxPriceLimit = maxPriceLimit }, CancellationToken.None);
            if (validationIssue)
            {
                result.Status.Should().Be(OperationStatus.InvalidValues);
                return;
            }
            result.Status.Should().Be(OperationStatus.Success);
        }
    }
}
