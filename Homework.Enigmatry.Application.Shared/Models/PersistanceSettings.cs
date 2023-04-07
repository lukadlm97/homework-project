using System.ComponentModel.DataAnnotations;

namespace Homework.Enigmatry.Application.Shared.Models
{
    public class PersistenceSettings
    {
        [Required]
        public bool UseInMemory { get; set; }
        [Required]
        public string Database { get; set;}
    }
}
