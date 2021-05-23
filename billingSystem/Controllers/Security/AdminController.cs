using Acorna.Core.Services;
using Acorna.DTOs.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Acorna.Controllers.Security
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public AdminController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [Authorize(Policy = "RequierUsersAdminRole")]
        [HttpGet("GetUsersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            try
            {
                return Ok(await _unitOfWorkService.SecurityService.GetUsersListAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Policy = "RequierUsersAdminRole")]
        [HttpPost]
        [Route("editRoles")]
        public async Task<IActionResult> EditRoles(string userName, RoleEdit roleEditDTO)
        {
            return Ok(await _unitOfWorkService.SecurityService.EditRoles(userName, roleEditDTO));
        }
    }
}
