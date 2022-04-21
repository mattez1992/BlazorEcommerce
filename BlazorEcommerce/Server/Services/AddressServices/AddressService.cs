using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Server.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _dataContext;
        private readonly IAuthService _authService;

        public AddressService(DataContext dataContext, IAuthService authService)
        {
            _dataContext = dataContext;
            _authService = authService;
        }
        public async Task<ServiceResponse<Address>> UpserAddress(CreateAddressDto createAddressDto)
        {
            var response = new ServiceResponse<Address>();
            var existingAddress = (await GetAddress()).Data;
            if (existingAddress == null)
            {
                Address newAddress = new()
                {
                    UserId = _authService.GetUserId(),
                    FirstName = createAddressDto.FirstName,
                    LastName = createAddressDto.LastName,
                    State = createAddressDto.State,
                    Country = createAddressDto.Country,
                    City = createAddressDto.City,
                    Zip = createAddressDto.Zip,
                    Street = createAddressDto.Street,
                };
                _dataContext.Addresses.Add(newAddress);
                response.Data = newAddress;
            }
            else
            {
                existingAddress.FirstName = createAddressDto.FirstName;
                existingAddress.LastName = createAddressDto.LastName;
                existingAddress.State = createAddressDto.State;
                existingAddress.Country = createAddressDto.Country;
                existingAddress.City = createAddressDto.City;
                existingAddress.Zip = createAddressDto.Zip;
                existingAddress.Street = createAddressDto.Street;
                response.Data = existingAddress;
            }

            await _dataContext.SaveChangesAsync();

            return response;
        }
        public async Task<ServiceResponse<Address>> GetAddress()
        {
            int userID = _authService.GetUserId();
            var address = await _dataContext.Addresses.FirstOrDefaultAsync(a => a.UserId == userID);
            return new ServiceResponse<Address> { Data = address };
        }
    }
}
