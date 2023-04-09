using FluentValidation;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Validators.Queries
{
    public class GetArticlesValidator : AbstractValidator<GetArticlesRequest>
    {
        public GetArticlesValidator()
        {
            RuleFor(request => request.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater then 0");
            RuleFor(request => request.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater then 0"); ;
        }
    }
}
