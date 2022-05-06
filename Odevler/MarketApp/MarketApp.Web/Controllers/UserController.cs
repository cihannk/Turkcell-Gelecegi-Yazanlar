using MarketApp.Business.Abstract;
using MarketApp.Dtos.Models;
using MarketApp.Dtos.Request;
using MarketApp.Dtos.Response;
using MarketApp.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketApp.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;

        public UserController(IUserService userService, IAddressService addressService, IOrderService orderService)
        {
            _userService = userService;
            _addressService = addressService;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Info");
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            if (returnUrl != null)
                ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.Login(model);
                    List<Claim> claims = new List<Claim> {
                        new Claim(type: "id", value: user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role.Name),
                        new Claim(ClaimTypes.Hash, System.Text.Encoding.UTF8.GetString(user.Salt))
                    };
                    await SignIn(claims);
                    if (returnUrl == null)
                        return Redirect("/");
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("login", ex.Message);
                }
            }
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.Register(model);
                    List<Claim> claims = new List<Claim> {
                        new Claim(type: "id", value: user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role.Name),
                        new Claim(ClaimTypes.Hash, System.Text.Encoding.UTF8.GetString(user.Salt))
                    };
                    await SignIn(claims);
                    return Redirect("/");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("login", ex.Message);
                }
                
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public async Task<IActionResult> Orders()
        {
            int userId = User.Identity.GetId();
            var orders = await _orderService.GetOrdersByUserId(userId);
            return View(orders);
        }
        public async Task<IActionResult> Addresses()
        {
            int userId = User.Identity.GetId();
            var addresses = await _addressService.GetUserAddressesWithUserId(userId);
            return View(addresses);
        }
        [HttpGet("User/Addresses/Delete/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            await _addressService.DeleteAddress(addressId);
            return RedirectToAction(nameof(Addresses));
        }
        [HttpGet("/User/Addresses/Add")]
        public async Task<IActionResult> AddAddress()
        {
            return View();
        }
        [HttpPost("User/Addresses/Add")]
        public async Task<IActionResult> AddAddress(AddAddressRequest address)
        {
            address.UserId = User.Identity.GetId();
            if (ModelState.IsValid)
            {
                await _addressService.AddAddress(address);
                return RedirectToAction(nameof(Addresses));
            }
            return View();
        }
        public async Task<IActionResult> Info()
        {
            int userId = User.Identity.GetId();
            var user = await _userService.GetUser(userId);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Info(GetUserResponse user)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(user);
                return RedirectToAction(nameof(Info));
            }
            else
            {
                ModelState.AddModelError("error", "yanlış formatta doldurdunuz");
                return View();
            }
            
        }
        public async Task<IActionResult> ChangePassword()
        {
            string email = User.Identity.GetEmail();
            ViewBag.Email = email;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.Login(new UserLoginModel { Email = model.Email, Password = model.Password });
                    await _userService.ChangePassword(user.Email, model.NewPassword);
                    return RedirectToAction(nameof(Info));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("error", "Bilgiler yanlış formatta gönderildi");
                return View();
            }
            
        }
        private async Task SignIn(List<Claim> claims)
        {
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(claimsPrincipal);
        }
    }
}
