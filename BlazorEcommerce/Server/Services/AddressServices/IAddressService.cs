
namespace BlazorEcommerce.Server.Services.AddressServices
{
    public interface IAddressService
    {
        Task<ServiceResponse<Address>> GetAddress();
        Task<ServiceResponse<Address>> UpserAddress(CreateAddressDto createAddressDto);
    }
}