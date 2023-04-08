using System.ComponentModel.DataAnnotations;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;
using MediatR;

namespace Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Commands
{
    public class RegisterRequest : IRequest<OperationResult<AuthDto>>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
