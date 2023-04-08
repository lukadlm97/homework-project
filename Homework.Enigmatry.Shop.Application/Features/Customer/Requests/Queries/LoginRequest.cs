using MediatR;
using System.ComponentModel.DataAnnotations;
using Homework.Enigmatry.Application.Shared.DTOs.Common;
using Homework.Enigmatry.Shop.Application.DTOs.Customer;

namespace Homework.Enigmatry.Shop.Application.Features.Customer.Requests.Queries
{
    public class LoginRequest : IRequest<OperationResult<AuthDto>>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
