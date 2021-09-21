using Acorna.Controllers.Base;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace billingSystem.Controllers.Project.BillingSystem
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComingServicesController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public ComingServicesController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetComingServices")]
        public async Task<IActionResult> GetComingServices(int pageIndex = 1, int pageSize = 10, int statusNumber = 1)
        {
            try
            {
                return Ok(await _unitOfWorkService.ComingServicesService.GetComingServices(pageIndex, pageSize, statusNumber, CurrentUserId, CurrentUserRole));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("ApproveServices")]
        public IActionResult ApproveServices(List<int> servicesId)
        {
            try
            {
                return Ok(_unitOfWorkService.ComingServicesService.ApproveServices(servicesId, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("InprogressServices")]
        public IActionResult InprogressServices(List<int> servicesId)
        {
            try
            {
                return Ok(_unitOfWorkService.ComingServicesService.InprogressServices(servicesId, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("RejectServices")]
        public IActionResult RejectServices(List<int> servicesId)
        {
            try
            {
                return Ok(_unitOfWorkService.ComingServicesService.RejectServices(servicesId, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
