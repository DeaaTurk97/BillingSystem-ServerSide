using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Controllers.Base;
using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Sheard;
using Acorna.Core.Services;
using Acorna.DTOs.Security;

namespace Acorna.Controllers.Security
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public UserController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<UserModel> userModels = new List<UserModel>();// await _unitOfWorkService.SecurityService.GetAllUsersAsync();
                return Ok(userModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllSuperAdmins")]
        public async Task<IActionResult> GetAllSuperAdminsAsync()
        {
            try
            {
                List<UserModel> userModels = await _unitOfWorkService.SecurityService.GetAllSuperAdminsAsync();
                return Ok(userModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdminsAsync()
        {
            try
            {
                List<UserModel> userModels = await _unitOfWorkService.SecurityService.GetAllAdminsAsync();
                return Ok(userModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _unitOfWorkService.SecurityService.GetAllUsersAsync(pageIndex, pageSize, CurrentUserId, CurrentUserRole));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserRegister userRegister)
        {
            try
            {
                return Ok(await _unitOfWorkService.SecurityService.AddUserAsync(userRegister));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserRegister userRegister)
        {
            try
            {
                return Ok(await _unitOfWorkService.SecurityService.UpdateUserAsync(userRegister));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                return Ok(await _unitOfWorkService.SecurityService.Delete(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateUserLanguage")]
        public async Task<IActionResult> UpdateUserLanguage(int languageId)
        {
            try
            {
                if (languageId != 0)
                    await _unitOfWorkService.SecurityService.UpdateUserLanguage(CurrentUserId, languageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok("true");
        }

        [HttpGet]
        [Route("GetLanguageInformations")]
        public async Task<IActionResult> GetLanguageInformations()
        {
            try
            {
                LanguageModel languageModel = await _unitOfWorkService.SecurityService.GetLanguageInformations(CurrentUserId);
                return Ok(languageModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                List<Role> roles = await _unitOfWorkService.SecurityService.GetAllRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(UserRegister userRegister)
        {
            try
            {
                return Ok(await _unitOfWorkService.SecurityService.UpdateUserRole(userRegister));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUsersByCurrentRole")]
        public async Task<IActionResult> GetUsersByCurrentRole()
        {
            try
            {
                var list = await _unitOfWorkService.SecurityService.GetUsersByCurrentRole(CurrentUserId, CurrentUserRole);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
