using Acorna.Controllers.Base;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace billingSystem.Controllers.Project.BillingSystem
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TypePhonesNumberController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public TypePhonesNumberController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllTypesPhoneNumber")]
        public async Task<IActionResult> GetAllTypesPhoneNumber()
        {
            try
            {
                return Ok(await _unitOfWorkService.TypePhoneNumberService.GetAllTypesPhoneNumber());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetAllTypesWithoutFreeType")]
        public async Task<IActionResult> GetAllTypesWithoutFreeType()
        {
            try
            {
                return Ok(await _unitOfWorkService.TypePhoneNumberService.GetAllTypesWithoutFreeType());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
