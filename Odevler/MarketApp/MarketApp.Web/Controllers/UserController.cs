using MarketApp.Business.Abstract;
using MarketApp.Dtos.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(string? returnUrl)
        {
            if (returnUrl != null)
                ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
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
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
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
        public async Task<IActionResult> AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private async Task SignIn(List<Claim> claims)
        {
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(claimsPrincipal);
        }
    }
}
