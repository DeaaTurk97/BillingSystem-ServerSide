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
    public class SimProfilesController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public SimProfilesController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }


        [HttpGet]
        [Route("GetAllSimProfiles")]
        public async Task<IActionResult> GetAllSimProfiles()
        {
            try
            {
                return Ok(await _unitOfWorkService.SimProfileService.GetAllSimProfile());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
