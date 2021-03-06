using Hangfire;
using MarketApp.API.Filters;
using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.SuccessMessages;
using MarketApp.Dtos.Models;
using MarketApp.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarketApp.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUser(id);
            return Ok(user);
        }
        [ModelValidation]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequest user)
        {
            await _userService.UpdateUser(user);
            return Ok(SuccessMessages.User.SuccessfullyUpdated);
        }
        [ModelValidation]
        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterModel userModel)
        {
            var user = await _userService.Register(userModel);
            return CreatedAtAction(nameof(GetById), routeValues: new { id = user.Id }, null);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);
            return Ok(SuccessMessages.User.SuccessfullyDeleted);
        }
        [ModelValidation]
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var user = await _userService.Login(model);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(type: "id", value: user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTSecretKey")));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            string token = GetToken(claims, credential, DateTime.Now.AddMinutes(15), DateTime.Now);
            return Ok(new { token = token });
        }
        [ModelValidation]
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var user = await _userService.Register(model);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(type: "id", value: user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTSecretKey")));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            string token = GetToken(claims, credential, DateTime.Now.AddMinutes(15), DateTime.Now);
            BackgroundJob.Enqueue(() => ThanksForRegister(user.Username));
            return Ok(new { token = token });
        }

        private string GetToken(Claim[] claims, SigningCredentials credentials, DateTime expires, DateTime notBefore)
        {
            var token = new JwtSecurityToken(
                issuer: "MarketApp",
                audience: "MarketApp",
                claims: claims,
                notBefore: notBefore,
                expires: expires,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void ThanksForRegister(string username)
        {
            Console.WriteLine($"Thanks for registering {username}");
        }

    }
}
