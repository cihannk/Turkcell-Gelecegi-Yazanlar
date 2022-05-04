using MarketApp.Business.Abstract;
using MarketApp.Dtos.Request;
using MarketApp.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketApp.Web.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = User.Identity.GetId();
            var addresses= await _addressService.GetUserAddressesWithUserId(userId);
            return View(addresses);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAddressRequest address)
        {
            address.UserId = User.Identity.GetId();
            if(ModelState.IsValid){
                await _addressService.AddAddress(address);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
