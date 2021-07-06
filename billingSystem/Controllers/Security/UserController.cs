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
                List<UserModel> userModels = await _unitOfWorkService.SecurityService.GetAllUsersAsync();
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
        [Route("Get")]
        public async Task<IActionResult> Get(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                PaginationRecord<UserModel> paginationRecord = new PaginationRecord<UserModel>
                {
                    DataRecord = await _unitOfWorkService.SecurityService.GetAllUsersAsync(pageIndex, pageSize),
                    CountRecord = _unitOfWorkService.SecurityService.GetUsersCountRecord(),
                };

                return Ok(paginationRecord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserModel userModel)
        {

            return Ok(userModel.UserId);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] UserModel userModel)
        {
            return Ok("true");
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
        public async Task<IActionResult> UpdateUserRole(UserModel userModel)
        {
            try
            {
                return Ok(await _unitOfWorkService.SecurityService.UpdateUserRole(userModel));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
