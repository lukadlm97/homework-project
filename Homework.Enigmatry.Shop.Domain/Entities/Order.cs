

namespace Homework.Enigmatry.Shop.Domain.Entities
{
    public class Order:BaseEntity
    {
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Article Article { get; set; }
        public int ArticleId { get; set; }

    }
}
