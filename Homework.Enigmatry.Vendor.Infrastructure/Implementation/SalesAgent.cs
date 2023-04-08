using Homework.Enigmatry.Application.Shared.Contracts;
using Homework.Enigmatry.Shop.Domain.Entities;
using Homework.Enigmatry.Vendor.Application.Contracts;

namespace Homework.Enigmatry.Vendor.Infrastructure.Implementation
{
    public class SalesAgent:ISalesAgent
    {
        private readonly IArticleRepository _articleRepository;

        public SalesAgent(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<bool> CheckInventory(int productId)
        {
            var randomNumber = new Random().Next(1000);
            if (randomNumber % 2 == 0)
            {
                var article = new Article()
                {
                    Id = productId,
                    Name = "product" + productId,
                    Price = new decimal(new Random(productId*100).NextDouble()),
                };
                await _articleRepository.Add(article);
                return true;
            }

            return false;
        }
    }
}
