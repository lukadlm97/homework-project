namespace Homework.Enigmatry.Shop.Application.DTOs
{
    public record PagingRequestDto(string? Filter = null, int PageNumber = 1, int PageSize = 10);
}
