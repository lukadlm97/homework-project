using Homework.Enigmatry.Shop.Domain.Entities;

namespace Homework.Enigmatry.Shop.Application.Contracts
{
    public interface ITokenService
    {
        string CreateToken(Customer  customer,string role);
    }
}
