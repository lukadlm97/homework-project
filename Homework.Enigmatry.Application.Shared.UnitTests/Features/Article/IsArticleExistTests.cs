using AutoMapper;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.Features.Articles.Handlers.Queries;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Application.Shared.UnitTests.Mocks;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Homework.Enigmatry.Application.Shared.Profiles;
using Homework.Enigmatry.Shop.Application.Features.Articles.Handlers.Queries;
using Homework.Enigmatry.Shop.Domain.Enums;

namespace Homework.Enigmatry.Application.Shared.UnitTests.Features.Article
{
    public class IsArticleExistTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _articleRepositoryMock;

        public IsArticleExistTests()
        {
            _articleRepositoryMock = MockArticleRepository.GetArticleRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task IsArticleExist_ExistingArticleIds_ReturnTrue(int id)
        {
            var handler = new IsArticleExistHandler(_articleRepositoryMock.Object, new LogTraceData());

            var result = await handler.Handle(new IsArticleExistRequest() { Id = id }, CancellationToken.None);

            result.Should().BeTrue();
        }

        [Xunit.Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(10)]
        public async Task IsArticleExist_NotExistingArticleIds_ReturnFalse(int id)
        {
            var handler = new IsArticleExistHandler(_articleRepositoryMock.Object, new LogTraceData());

            var result = await handler.Handle(new IsArticleExistRequest() { Id = id }, CancellationToken.None);

            result.Should().BeFalse();
        }
    }
}
