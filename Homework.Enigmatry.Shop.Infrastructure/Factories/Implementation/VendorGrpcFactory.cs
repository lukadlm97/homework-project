using Grpc.Net.Client;
using Homework.Enigmatry.Shop.Application.Models;
using Homework.Enigmatry.Shop.Infrastructure.Factories.Contract;
using Homework.Enigmatry.Shop.VendorGrpcAPI;
using Microsoft.Extensions.Options;

namespace Homework.Enigmatry.Shop.Infrastructure.Factories.Implementation
{
    public class VendorGrpcFactory:IVendorGrpcFactory
    {
        private readonly ExternalGrpcSettings _externalGrpcSettings;
        private Vendor.VendorClient _vendorClient;

        public VendorGrpcFactory(IOptions<ExternalGrpcSettings> options)
        {
            _externalGrpcSettings = options.Value;
            CreateVendorClient();
        }
        public Vendor.VendorClient GetVendorClient()
        {
            if (_vendorClient == null)
            {
                CreateVendorClient();
            }

            return _vendorClient;
        }

        private void CreateVendorClient()
        {
            _vendorClient = new Vendor.VendorClient(GrpcChannel.ForAddress(_externalGrpcSettings.VendorServiceUrl));
        }
    }
}
