using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Enigmatry.Shop.Domain.Entities
{
    public class Customer:BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
