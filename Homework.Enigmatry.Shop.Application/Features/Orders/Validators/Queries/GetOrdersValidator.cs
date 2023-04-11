using FluentValidation;
using Homework.Enigmatry.Shop.Application.Features.Orders.Requests.Queries;

namespace Homework.Enigmatry.Shop.Application.Features.Orders.Validators.Queries
{
    public class GetOrdersValidator : AbstractValidator<GetOrdersRequest>
    {
        public GetOrdersValidator()
        {
            RuleFor(request => request.PageSize)
                .GreaterThan(0)
                .LessThan(int.MaxValue)
                .WithMessage("Page size must be greater then 0 and less then int.MAX");
            RuleFor(request => request.PageNumber)
                .GreaterThan(0)
                .LessThan(int.MaxValue)
                .WithMessage("Page number must be greater then 0 and less then int.MAX"); ;
        }
    }
}
