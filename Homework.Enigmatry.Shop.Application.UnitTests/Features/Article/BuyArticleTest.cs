using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.UnitTests.Mocks;
using Moq;
using System.Runtime.Caching;
using FluentAssertions;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Application.Shared.Models;
using Homework.Enigmatry.Shop.Application.Profiles;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.DTOs.Order;
using Homework.Enigmatry.Shop.Application.Exceptions;
using Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Commands;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
using Homework.Enigmatry.Shop.Domain.Enums;
using Microsoft.Extensions.Options;

namespace Homework.Enigmatry.Shop.Application.UnitTests.Features.Article
{
    public class BuyArticleTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly MemoryCache _memoryCache;
        private readonly Mock<IArticleRepository> _articleRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IOptions<PersistenceSettings> _persistenceOptions;
        private const decimal MaxLimitPricePass = 2200;
        private const int ArticleIdPass = 2;

        public BuyArticleTest()
        {
            _orderRepositoryMock = MockOrderRepository.GetOrderRepositoryMock();
            _customerRepositoryMock = MockCustomerRepository.GetCustomerRepositoryMock();
            _articleRepositoryMock = MockArticleRepository.GetArticleRepository();
            _memoryCache = MockMemoryCache.GetMemoryCache();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
                c.AddProfile<Homework.Enigmatry.Application.Shared.Profiles.MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _unitOfWork =
                MockUnitOfWork.GetUnitOfWork(_articleRepositoryMock.Object, _customerRepositoryMock.Object, _orderRepositoryMock.Object);

            _persistenceOptions = Options.Create(new PersistenceSettings(){UseInMemory = true});
        }

        [Xunit.Theory]
        [InlineData(11,1)]
        [InlineData(12,2)]
        [InlineData(13,2)]
        public async Task BuyArticle_ExistingArticlesAtCacheAndCustomers_ExecuteBuy(int articleId,int customerId)
        {
            var handler = new BuyArticleCommand(_memoryCache,_unitOfWork.Object,_mapper,new LogTraceData(), _persistenceOptions);

            var result = await handler.Handle(new BuyArticleRequest() { ArticleId = articleId,CustomerId = customerId}, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
        }

        [Xunit.Theory]
        [InlineData(11, 1)]
        [InlineData(12, 2)]
        [InlineData(13, 2)]
        public async Task BuyArticle_ExistingCustomersAndNotArticlesAtCache_OccurredUnavailableAtLocalPersistenceStorageException(int articleId, int customerId)
        {
            var persistenceOptions = Options.Create(new PersistenceSettings());
            var handler = new BuyArticleCommand(_memoryCache, _unitOfWork.Object, _mapper, new LogTraceData(), persistenceOptions);

            Func<Task<OperationResult<OrderDto>>> act =  () =>  
                  handler.Handle(new BuyArticleRequest() { ArticleId = articleId, CustomerId = customerId }, CancellationToken.None);

            await Assert.ThrowsAsync<UnavailableAtLocalPersistenceStorageException>(act);

        }

        [Xunit.Theory]
        [InlineData(111, 3)]
        [InlineData(12, 3)]
        [InlineData(111, 2)]
        public async Task BuyArticle_NotExistingCustomersOrArticles_ReturnInvalidValues(int articleId, int customerId)
        {
            var handler = new BuyArticleCommand(_memoryCache, _unitOfWork.Object, _mapper, new LogTraceData(), _persistenceOptions);
            var result = await handler.Handle(new BuyArticleRequest() { ArticleId = articleId, CustomerId = customerId }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.InvalidValues);
        }

        [Xunit.Theory]
        [InlineData(11111, 1)]
        public async Task BuyArticle_ExistingCustomerAndArticleAndOrder_ReturnArticleSold(int articleId, int customerId)
        {
            var handler = new BuyArticleCommand(_memoryCache, _unitOfWork.Object, _mapper, new LogTraceData(), _persistenceOptions);

            var result = await handler.Handle(new BuyArticleRequest() { ArticleId = articleId, CustomerId = customerId }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.ArticleSold);
        }

    }
}
