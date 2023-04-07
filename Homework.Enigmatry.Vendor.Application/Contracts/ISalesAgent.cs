namespace Homework.Enigmatry.Vendor.Application.Contracts
{
    public interface ISalesAgent
    {
        Task<bool> CheckInventory(int productId);
    }
}
