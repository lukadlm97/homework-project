﻿using Homework.Enigmatry.Shop.Application.Constants;
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
                Password = "$2b$10$2d0SUdDlNUAf8PYaEk3HduN.7Njy/jB8avpjBWgveQHT2CUbi6L9G",
                Role =  Constants.AdminRole
            },
            new Customer()
            {
                Id = 2,
                Username = "acc-test-two@gmail.com",
                Password = "$2b$10$2d0SUdDlNUAf8PYaEk3HduN.7Njy/jB8avpjBWgveQHT2CUbi6L9G",
                Role =  Constants.AdminRole
            }, 
            new Customer()
            {
                Id = 3,
                Username = "acc-test-three@gmail.com",
                Password = "$2b$10$2d0SUdDlNUAf8PYaEk3HduN.7Njy/jB8avpjBWgveQHT2CUbi6L9G",
                Role=
                    Constants.CustomerRole
            }
        };

        public List<Article> Articles = new List<Article>()
        {
            new Article()
            {
                Id = 1,
                Name = "article 1",
                Price = new decimal(Math.Round(new Random(1).NextDouble() * 100)),
            },
            new Article()
            {    
                Id = 2,
                Name = "article 2",
                Price = new decimal(Math.Round(new Random(2).NextDouble() * 100)),
            },
            new Article()
            {
                Id = 3, 
                Name = "article 3",
                Price = new decimal(Math.Round(new Random(3).NextDouble() * 100)),
            }
        };
        
    }
    
}
