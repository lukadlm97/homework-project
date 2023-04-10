using System.Text.Json.Serialization;
using Homework.Enigmatry.Application.Shared.DTOs.Common;

namespace Homework.Enigmatry.Application.Shared.DTOs.Article
{
    public record ArticleDetailsDto([property:JsonPropertyName("id")]int Id, [property: JsonPropertyName("name")] string Name, [property: JsonPropertyName("price")] decimal Price) : BaseDto(Id);
}
