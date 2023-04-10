using System.Text.Json.Serialization;

namespace Homework.Enigmatry.Application.Shared.DTOs.Common
{
    public record BaseDto([property: JsonPropertyName("id")] int Id);
}
