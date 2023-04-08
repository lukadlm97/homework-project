using System.ComponentModel.DataAnnotations;

namespace Homework.Enigmatry.Shop.Application.Models
{
    public class CacheSettings
    {
        [Required]
        public bool SlidingExpiration { get; set; }
        [Required]
        public TimeSpan SlidingExpirationTime { get; set; }
        [Required]
        public TimeSpan AbsoluteExpirationTime { get; set; }
    }
}
