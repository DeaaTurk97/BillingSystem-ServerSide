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
    public class ServicesTypesController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public ServicesTypesController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllServicesTypes")]
        public async Task<IActionResult> GetAllServicesTypes()
        {
            try
            {
                return Ok(await _unitOfWorkService.ServiceTypeService.GetAllServicesTypes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetServicesTypes")]
        public async Task<IActionResult> GetServicesTypes(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _unitOfWorkService.ServiceTypeService.GetServicesTypes(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetServiceTypeId")]
        public async Task<IActionResult> GetServiceTypeId(int serviceTypeId)
        {
            try
            {
                return Ok(await _unitOfWorkService.ServiceTypeService.GetServiceTypeId(serviceTypeId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddServiceType")]
        public IActionResult AddServiceType(ServiceTypeModel serviceTypeModel)
        {
            try
            {
                return Ok(_unitOfWorkService.ServiceTypeService.AddServiceType(serviceTypeModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateServiceType")]
        public IActionResult UpdateServiceType(ServiceTypeModel serviceTypeModel)
        {
            try
            {
                return Ok(_unitOfWorkService.ServiceTypeService.UpdateServiceType(serviceTypeModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeleteServiceType")]
        public IActionResult DeleteServiceType(int id)
        {
            try
            {
                return Ok(_unitOfWorkService.ServiceTypeService.DeleteServiceType(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
