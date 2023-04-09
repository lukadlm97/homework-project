using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Profiles;
using Homework.Enigmatry.Shop.Application.UnitTests.Mocks;
using Homework.Enigmatry.Application.Shared.Features.Articles.Handlers.Queries;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Extensions;
using Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Application.Models;
using Homework.Enigmatry.Shop.Domain.Enums;
using Homework.Enigmatry.Shop.Infrastructure.Services.Vendor;
using Microsoft.Extensions.Options;
using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Features.Article
{
    public class GetArticleOfferTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _articleRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IVendorRepository> _vendorRepositoryMock;
        private readonly Mock<IVendorGrpcRepository> _grpcVendor;
        private readonly MemoryCache _memoryCache;

        public GetArticleOfferTest()
        {
            _articleRepositoryMock = MockArticleRepository.GetArticleRepository();
            _orderRepositoryMock = MockOrderRepository.GetOrderRepositoryMock();
            _vendorRepositoryMock = MockVendorRepository.GetVendorRepositoryMock();
            _grpcVendor = MockVendorGrpcRepository.GetVendorRepositoryMock();
            _memoryCache = new MemoryCache("cache");
            var articles = new List<Domain.Entities.Article>()
            {
                new Domain.Entities.Article()
                {
                    Id = 1,
                    Name = "article 1",
                    Price = new decimal(new Random(100).NextDouble())
                },
                new Domain.Entities.Article()
                {
                    Id = 2,
                    Name = "article 2",
                    Price = new decimal(new Random(200).NextDouble())
                },
                new Domain.Entities.Article()
                {
                    Id = 3,
                    Name = "article 3",
                    Price = new decimal(new Random(300).NextDouble())
                }
            };
            foreach (var article in articles)
            {
                _memoryCache.Add(article.Id.CreateArticleCacheKey(), article, new CacheItemPolicy()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(20)
                });
            }

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
                c.AddProfile<Homework.Enigmatry.Application.Shared.Profiles.MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }


        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetArticleOfferById_ShouldReturnArticleFromCache(int id)
        {
            var vendorOptions = Options.Create(new VendorSettings());
            var vendorService = new VendorService(vendorOptions, _vendorRepositoryMock.Object,_grpcVendor.Object,new LogTraceData());

            var cacheItemSettings = Options.Create(new CacheSettings());
            var handler = new GetArticleOfferByIdHandler(_articleRepositoryMock.Object,vendorService,_mapper,_memoryCache, cacheItemSettings, _orderRepositoryMock.Object,new LogTraceData());

            var result = await handler.Handle(new GetArticleOfferByIdRequest() { Id = id,MaxPriceLimit = 120}, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
            result.Result.Name.Should().Be(string.Format("article {0}", id));
        }
    }
}
