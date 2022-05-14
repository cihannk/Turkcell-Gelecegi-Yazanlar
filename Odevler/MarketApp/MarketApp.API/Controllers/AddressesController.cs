using MarketApp.Business.Abstract;
using MarketApp.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _addressService.GetAllAddresses();
            return Ok(addresses);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _addressService.GetAddressById(id);
            return Ok(address);
        }
        [HttpGet("Username/{username}")]
        public async Task<IActionResult> GetAddressesByUsername(string username)
        {
            var addresses = await _addressService.GetUserAddressesWithUsername(username);
            return Ok(addresses);
        }
        [HttpGet("UserId/{userId}")]
        public async Task<IActionResult> GetAddressesByUserId(int userId)
        {
            var addresses = await _addressService.GetUserAddressesWithUserId(userId);
            return Ok(addresses);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAddressRequest address)
        {
            if (ModelState.IsValid)
            {
                await _addressService.UpdateAddress(address);
                return Ok("Adres başarıyla güncellendi");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _addressService.DeleteAddress(id);
            return Ok("Adres başarıyla silindi");
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddAddressRequest address)
        {
            int addressId = await _addressService.AddAddress(address);
            return CreatedAtAction(nameof(GetById), routeValues: new { id = addressId }, null);
        }
    }
}
