using System.ComponentModel.DataAnnotations;

namespace Homework.Enigmatry.Shop.Application.DTOs.Article
{
    public record ArticleRequestDto([Required]decimal MaxArticlePrice);
}
