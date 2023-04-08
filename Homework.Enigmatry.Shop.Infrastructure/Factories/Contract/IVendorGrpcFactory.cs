using Homework.Enigmatry.Shop.VendorGrpcAPI;

namespace Homework.Enigmatry.Shop.Infrastructure.Factories.Contract
{
    public interface IVendorGrpcFactory
    {
        Vendor.VendorClient GetVendorClient();
    }
}
