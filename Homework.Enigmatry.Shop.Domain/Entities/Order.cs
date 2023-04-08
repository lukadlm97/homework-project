namespace Homework.Enigmatry.Shop.Domain.Entities
{
    public class Order:BaseEntity
    {
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Article Article { get; set; }
        public int ArticleId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
