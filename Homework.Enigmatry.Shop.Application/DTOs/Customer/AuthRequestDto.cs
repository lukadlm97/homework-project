using System.ComponentModel.DataAnnotations;

namespace Homework.Enigmatry.Shop.Application.DTOs.Customer
{
    public record AuthRequestDto([Required] string Username,
        [Required] string Password);
}
