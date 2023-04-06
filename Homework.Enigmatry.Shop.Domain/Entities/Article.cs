using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Enigmatry.Shop.Domain.Entities
{
    public class Article:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public DateTime SoldDate { get; set; }

    }
}
