using Homework.Enigmatry.Application.Shared.DTOs.Common;

namespace Homework.Enigmatry.Application.Shared.DTOs.Article
{
    public record ArticleDetailsDto(int Id,string Name, decimal Price) : BaseDto(Id);
}
