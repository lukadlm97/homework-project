using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Homework.Enigmatry.Shop.Application.Profiles;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Commands;
using Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Shop.Application.Models;
using Homework.Enigmatry.Shop.Domain.Enums;
using Homework.Enigmatry.Shop.Infrastructure.Services.Vendor;
using Microsoft.Extensions.Options;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Features.Article
{
    public class BuyArticleTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly MemoryCache _memoryCache;
        private const decimal MaxLimitPricePass = 2200;
        private const int ArticleIdPass = 2;

        public BuyArticleTest()
        {
            _orderRepositoryMock = MockOrderRepository.GetOrderRepositoryMock();
            _customerRepositoryMock = MockCustomerRepository.GetCustomerRepositoryMock();
            _memoryCache = MockMemoryCache.GetMemoryCache();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
                c.AddProfile<Homework.Enigmatry.Application.Shared.Profiles.MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Xunit.Theory]
        [InlineData(11,1)]
        [InlineData(12,2)]
        [InlineData(13,2)]
        public async Task BuyArticleTest_ShouldExecuteBuy(int articleId,int customerId)
        {
            var handler = new BuyArticleCommand(_memoryCache,_customerRepositoryMock.Object,_orderRepositoryMock.Object,_mapper,new LogTraceData());

            var result = await handler.Handle(new BuyArticleRequest() { ArticleId = articleId,CustomerId = customerId}, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
        }

        [Xunit.Theory]
        [InlineData(111, 3)]
        [InlineData(12, 3)]
        [InlineData(111, 2)]
        public async Task BuyArticleTest_ShouldOccurredInvalidValuesIssue(int articleId, int customerId)
        {
            var handler = new BuyArticleCommand(_memoryCache, _customerRepositoryMock.Object, _orderRepositoryMock.Object, _mapper, new LogTraceData());

            var result = await handler.Handle(new BuyArticleRequest() { ArticleId = articleId, CustomerId = customerId }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.InvalidValues);
        }

        [Xunit.Theory]
        [InlineData(11111, 1)]
        public async Task BuyArticleTest_ShouldArticleSold(int articleId, int customerId)
        {
            var handler = new BuyArticleCommand(_memoryCache, _customerRepositoryMock.Object, _orderRepositoryMock.Object, _mapper, new LogTraceData());

            var result = await handler.Handle(new BuyArticleRequest() { ArticleId = articleId, CustomerId = customerId }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.ArticleSold);
        }

    }
}
