using FluentValidation;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Validators.Queries
{
    public class GetArticleOfferByIdValidator:AbstractValidator<GetArticleOfferByIdRequest>
    {
        public GetArticleOfferByIdValidator()
        {
            RuleFor(request => request).NotNull()
                                                .WithMessage("Your get offers for article by id request cannot be empty"); 
            RuleFor(article => article.Id);
            RuleFor(article => article.MaxPriceLimit)
                .GreaterThan(0)
                .LessThan(decimal.MaxValue)
                .WithMessage("Max price limit should be greater than 0"); 
        }

    }
}
