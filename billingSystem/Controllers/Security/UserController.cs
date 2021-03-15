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

namespace Acorna.Controllers.Security
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : TeamControllerBase
    {
        private readonly ISecurityService _securityService;

        public UserController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<UserModel> userModels = await _securityService.GetAllUsersAsync();
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
                List<UserModel> userModels = await _securityService.GetAllSuperAdminsAsync();
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
                List<UserModel> userModels = await _securityService.GetAllAdminsAsync();
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
                    DataRecord = await _securityService.GetAllUsersAsync(pageIndex, pageSize),
                    CountRecord = _securityService.GetCountRecord(),
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
        public IActionResult Delete(int id)
        {
            try
            {
                _securityService.Delete(id);
            }
            catch (Exception)
            {
                BadRequest("false");
            }

            return Ok("true");
        }

        [HttpPut]
        [Route("UpdateUserLanguage")]
        public async Task<IActionResult> UpdateUserLanguage(int languageId)
        {
            try
            {
                if (languageId != 0)
                    await _securityService.UpdateUserLanguage(CurrentUserId, languageId);
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
                LanguageModel languageModel = await _securityService.GetLanguageInformations(CurrentUserId);
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
                List<Role> roles = await _securityService.GetAllRoles();
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
                return Ok(await _securityService.UpdateUserRole(userModel));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
