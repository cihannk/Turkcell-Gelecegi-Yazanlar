using MarketApp.API.Filters;
using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.SuccessMessages;
using MarketApp.Dtos.Request;
using MarketApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MarketApp.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Roles ="Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMemoryCache _memoryCache;

        public RolesController(IRoleService roleService, IMemoryCache memoryCache)
        {
            _roleService = roleService;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _memoryCache.GetOrCreateAsync("roles", async entry =>
            {
                entry.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
                return await _roleService.GetAllRoles();
            });
            //var roles = await _roleService.GetAllRoles();
            return Ok(roles);
        }
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetById(int roleId)
        {
            var role = await _memoryCache.GetOrCreateAsync($"role{roleId}", async (entry) =>
            {
                entry.AbsoluteExpiration = DateTime.Now.AddMinutes(15);
                return await _roleService.GetRoleById(roleId);
            });
            return Ok(role);
        }
        [ModelValidation]
        [HttpPut]
        public async Task<IActionResult> Update(Role role)
        {
            await _roleService.UpdateRole(role);
            return Ok(SuccessMessages.Role.SuccessfullyUpdated);
        }
        [ModelValidation]
        [HttpPost]
        public async Task<IActionResult> Create(AddRoleRequest role)
        {
            var roleId = await _roleService.AddRole(role);
            return CreatedAtAction(nameof(GetById), routeValues: new { roleId = roleId }, null);
        }
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> Delete(int roleId)
        {
            await _roleService.DeleteRole(roleId);
            return Ok(SuccessMessages.Role.SuccessfullyDeleted);
        }
    }
}
