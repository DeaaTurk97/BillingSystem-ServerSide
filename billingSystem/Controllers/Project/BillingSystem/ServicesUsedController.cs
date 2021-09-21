using Acorna.Core.Models.Project.BillingSystem;
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
    public class ServicesUsedController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public ServicesUsedController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllServicesUsed")]
        public async Task<IActionResult> GetAllServicesUsed()
        {
            try
            {
                return Ok(await _unitOfWorkService.ServiceUsedService.GetAllServicesUsed());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetServicesUsed")]
        public async Task<IActionResult> GetServicesUsed(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _unitOfWorkService.ServiceUsedService.GetServicesUsed(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetServiceUsedId")]
        public async Task<IActionResult> GetServiceUsedId(int serviceUsedId)
        {
            try
            {
                return Ok(await _unitOfWorkService.ServiceUsedService.GetServiceUsedId(serviceUsedId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddServiceUsed")]
        public IActionResult AddServiceUsed(ServiceUsedModel serviceUsedModel)
        {
            try
            {
                return Ok(_unitOfWorkService.ServiceUsedService.AddServiceUsed(serviceUsedModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateServiceUsed")]
        public IActionResult UpdateServiceUsed(ServiceUsedModel serviceUsedModel)
        {
            try
            {
                return Ok(_unitOfWorkService.ServiceUsedService.UpdateServiceUsed(serviceUsedModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeleteServiceUsed")]
        public IActionResult DeleteServiceUsed(int id)
        {
            try
            {
                return Ok(_unitOfWorkService.ServiceUsedService.DeleteServiceUsed(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
