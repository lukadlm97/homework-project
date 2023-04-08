using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Queries;

namespace Homework.Enigmatry.Shop.Application.Features.Articles.Validators.Queries
{
    public class GetArticleOfferByIdValidator:AbstractValidator<GetArticleOfferByIdRequest>
    {
        public GetArticleOfferByIdValidator()
        {
            RuleFor(article => article).NotNull();
            RuleFor(article => article.Id).NotEmpty().GreaterThan(0);
            RuleFor(article => article.MaxPriceLimit).GreaterThan(0);
        }

    }
}
