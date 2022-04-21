
namespace BlazorEcommerce.Client.Services.AddressServices
{
    public interface IAddressService
    {
        Task<Address> AddOrUpdateAddress(Address address);
        Task<Address> GetAddress();
    }
}