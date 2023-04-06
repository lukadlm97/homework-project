using System.ComponentModel.DataAnnotations;

namespace Homework.Enigmatry.Shop.Application.Models
{
    public class PersistenceSettings
    {
        [Required]
        public bool UseInMemory { get; set; }
        [Required]
        public string Database { get; set;}
    }
}
