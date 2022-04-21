using BlazorEcommerce.Server.Services.AddressServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/address")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _adressService;

        public AddressController(IAddressService adressService)
        {
            _adressService = adressService;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Address>>> GetAddress()
        {
            return await _adressService.GetAddress();
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Address>>> AddOrUpdateAddress(CreateAddressDto createAddressDto)
        {
            return await _adressService.UpserAddress(createAddressDto);
        }
    }
}
