using System.ComponentModel.DataAnnotations;

namespace Homework.Enigmatry.Shop.Application.Models
{
    public class ExternalGrpcSettings
    {
        [Url]
        public string VendorServiceUrl { get; set; }
    }
}
