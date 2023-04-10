using AutoMapper;
using FluentAssertions;
using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Application.Shared.Features.Articles.Handlers.Queries;
using Homework.Enigmatry.Application.Shared.Features.Articles.Requests.Queries;
using Homework.Enigmatry.Application.Shared.Profiles;
using Homework.Enigmatry.Application.Shared.UnitTests.Mocks;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Domain.Enums;
using Moq;

namespace Homework.Enigmatry.Application.Shared.UnitTests.Features.Article
{
    public class GetArticleByIdHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IArticleRepository> _articleRepositoryMock;

        public GetArticleByIdHandlerTests()
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
        public async Task GetArticleById_ShouldReturnArticle(int id)
        {
            var handler = new GetArticleByIdHandler(_articleRepositoryMock.Object,_mapper,new LogTraceData());

            var result = await handler.Handle(new GetArticleByIdRequest(){Id = id}, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.Success);
            result.Result.Name.Should().Be(string.Format("article {0}",id));
        }

        [Xunit.Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(10)]
        public async Task GetArticleById_ShouldNotReturnArticle(int id)
        {
            var handler = new GetArticleByIdHandler(_articleRepositoryMock.Object, _mapper, new LogTraceData());

            var result = await handler.Handle(new GetArticleByIdRequest() { Id = id }, CancellationToken.None);

            result.Status.Should().Be(OperationStatus.NotExist);
        }



    }
}
