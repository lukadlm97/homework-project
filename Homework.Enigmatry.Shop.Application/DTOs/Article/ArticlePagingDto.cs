namespace Homework.Enigmatry.Shop.Application.DTOs.Article
{
    public record ArticlePagingDto(string? Filter=null,int PageNumber=1,int PageSize=10);
}
