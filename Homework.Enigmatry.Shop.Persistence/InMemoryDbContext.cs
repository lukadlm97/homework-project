using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class InMemoryDbContext<T> where T : BaseEntity
    {
        public List<T> List = new List<T>();
    }


    public class InMemoryDbContext
    {
        public List<Customer> Customers=new List<Customer>()
        {
            new Customer()
            {
                Id = 1,
                Username = "acc-test-one@gmail.com",
                Password = "passA1ds"
            },
            new Customer()
            {
                Id = 2,
                Username = "acc-test-two@gmail.com",
                Password = "passA2ds"
            }, 
            new Customer()
            {
                Id = 3,
                Username = "acc-test-three@gmail.com",
                Password = "passA3ds"
            }
        };

        public List<Article> Articles = new List<Article>()
        {
            new Article()
            {
                Id = 1,
                Name = "article 1",
                Price = new decimal(new Random(100).NextDouble())
            },
            new Article()
            {    
                Id = 2,
                Name = "article 2",
                Price = new decimal(new Random(200).NextDouble())
            },
            new Article()
            {
                Id = 3, 
                Name = "article 3",
                Price = new decimal(new Random(300).NextDouble())
            }
        };
        
    }
    
}
