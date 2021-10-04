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
    public class SimCardTypesController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public SimCardTypesController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllSimCardTypes")]
        public async Task<IActionResult> GetAllSimCardTypes()
        {
            try
            {
                return Ok(await _unitOfWorkService.SimCardTypeService.GetAllSimCardType());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
