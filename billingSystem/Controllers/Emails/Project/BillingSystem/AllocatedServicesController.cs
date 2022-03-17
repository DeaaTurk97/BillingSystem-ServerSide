using Acorna.Controllers.Base;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace billingSystem.Controllers.Project.BillingSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocatedServicesController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public AllocatedServicesController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllAllocatedServices")]
        public IActionResult GetAllAllocatedServices(int userId)
        {
            try
            {
                return Ok(_unitOfWorkService.AllocatedServicesService.GetAllAlocatedServices(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
