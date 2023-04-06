using System.ComponentModel.DataAnnotations;

namespace Homework.Enigmatry.Shop.Application.Models
{
    public class VendorSettings
    {
        [Required]
        public string FirstVendorHttpClientName { get; set; }
        [Required]
        public string SecoundVendorHttpClientName { get; set; }
        [Url]
        public string FirstVendorUrl { get; set; }
        [Url]
        public string SecoundVendorUrl { get; set; }
    }
}
