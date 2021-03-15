using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Acorna.Core.Entity.Security;
using Acorna.DTO.Security;

namespace Acorna.Controllers.Security
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly UserManager<User> _userManager;

        public AdminController(ISecurityService securityService, UserManager<User> userManager)
        {
            _securityService = securityService;
            _userManager = userManager;
        }

        [Authorize(Policy = "RequierSuperAdminRole")]
        [HttpGet("GetUsersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            try
            {
                return Ok(await _securityService.GetUsersListAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Policy = "RequierSuperAdminRole")]
        [HttpPost]
        [Route("editRoles")]
        public async Task<IActionResult> EditRoles(string userName, RoleEdit roleEditDTO)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = roleEditDTO.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { }; // same ---> selectedRoles = selectedRoles != null ? selectedRoles : new string[] {};
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Something goes wrong with adding roles!");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Something goes wrong with removing roles!");


            return Ok(await _userManager.GetRolesAsync(user));

        }
    }
}
