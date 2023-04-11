using Homework.Enigmatry.Application.Shared.DTOs.Article;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;

namespace Homework.Enigmatry.Shop.Application.DTOs.Order
{
    public record OrderDto(int Id, ArticleDto ArticleDto, decimal Price,CustomerDto CustomerDto);
}
