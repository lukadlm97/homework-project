using System.Runtime.Caching;
using FluentValidation;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Extensions;
using Homework.Enigmatry.Shop.Application.Features.Articles.Requests.Commands;
namespace Homework.Enigmatry.Shop.Application.Features.Articles.Validators.Commands
{
    public class BuyArticleValidator : AbstractValidator<BuyArticleRequest>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly MemoryCache _memoryCache;

        public BuyArticleValidator(ICustomerRepository customerRepository,MemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _customerRepository = customerRepository;

            RuleFor(request => request).NotNull()
                                                .WithMessage("Your buy article request cannot be empty");

            RuleFor(request => request.CustomerId)
                .MustAsync(async (id, token) =>
                {
                    return await _customerRepository.Exists(id);
                }).WithMessage("You must be register as customer.")
                .MustAsync(async (id, token) =>
                {
                    var customer= await _customerRepository.Get(id);
                    return customer != null &&
                           string.Compare(customer.Role,Constants.Constants.CUSTOMER_ROLE,StringComparison.InvariantCulture)==0;
                }).WithMessage("Customer must be authorized at customer role.");
            RuleFor(request => request.ArticleId)
                .MustAsync(async (id, token) =>
                {
                    var key = id.CreateArticleCacheKey();
                    object? itemFromCache = _memoryCache.Get(key);
                    return itemFromCache != null;
                }).WithMessage("Article must be available at cache.");

        }
    }
}
