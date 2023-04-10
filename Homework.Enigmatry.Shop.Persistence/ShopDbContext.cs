using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Homework.Enigmatry.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework.Enigmatry.Persistence.Shared
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);
        }
    }
}
