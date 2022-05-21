using MarketApp.API.Extensions;
using MarketApp.API.Filters;
using MarketApp.API.Models;
using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.ErrorMessages;
using MarketApp.Business.Constants.SuccessMessages;
using MarketApp.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _addressService.GetAllAddresses();
            return Ok(addresses);
        }
        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _addressService.GetAddressById(id);
            return Ok(address);
        }
        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet("Username/{username}")]
        public async Task<IActionResult> GetAddressesByUsername(string username)
        {
            var addresses = await _addressService.GetUserAddressesWithUsername(username);
            return Ok(addresses);
        }
        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet("UserId/{userId}")]
        public async Task<IActionResult> GetAddressesByUserId(int userId)
        {
            var addresses = await _addressService.GetUserAddressesWithUserId(userId);
            return Ok(addresses);
        }
        [ModelValidation]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAddressRequest address)
        {
            await _addressService.UpdateAddress(address);
            return Ok(SuccessMessages.Address.SuccessfullyUpdated);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _addressService.DeleteAddress(id);
            return Ok(SuccessMessages.Address.SuccessfullyDeleted);
        }
        [ModelValidation]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(AddAddressRequest address)
        {
            int addressId = await _addressService.AddAddress(address);
            return CreatedAtAction(nameof(GetById), routeValues: new { id = addressId }, null);
        }
        //Controllers for user
        [ModelValidation]
        [Authorize]
        [HttpPost("User")]
        public async Task<IActionResult> AddAddressUser(UserAddAddressModel address)
        {
            await _addressService.AddAddress(
            new AddAddressRequest
            {
                UserId = User.Identity.GetId(),
                AddressDetail = address.AddressDetail,
                City = address.City,
                DesiredName = address.DesiredName,
                District = address.District,
                Street = address.Street,
            });
            return Ok(SuccessMessages.Address.SuccessfullyCreated);

        }
        [Authorize]
        [HttpGet("User")]
        public async Task<IActionResult> GetAddressesUser()
        {
            return Ok(await _addressService.GetUserAddressesWithUserId(User.Identity.GetId()));
        }
        [ModelValidation]
        [Authorize]
        [HttpPut("User")]
        public async Task<IActionResult> UpdateAddressUser(UserUpdateAddressModel address)
        {
            int userId = User.Identity.GetId();
            if (await _addressService.CheckIfUserHasAddress(userId, address.Id))
            {
                await _addressService.UpdateAddress(new UpdateAddressRequest {
                    Id= address.Id,
                    UserId= userId,
                    AddressDetail = address.AddressDetail,
                    City= address.City,
                    DesiredName= address.DesiredName,
                    District= address.District,
                    Street= address.Street
                });
                return Ok(SuccessMessages.Address.SuccessfullyUpdated);
            }
            return BadRequest(ErrorMessages.Address.NotBelongToToken);
        }
        [Authorize]
        [HttpDelete("User/{addressId}")]
        public async Task<IActionResult> DeleteAddressUser(int addressId)
        {
            if(await _addressService.CheckIfUserHasAddress(User.Identity.GetId(), addressId))
            {
                await _addressService.DeleteAddress(addressId);
                return Ok(SuccessMessages.Address.SuccessfullyDeleted);
            }
            return BadRequest(ErrorMessages.Address.NotBelongToToken);
        }
    }
}
