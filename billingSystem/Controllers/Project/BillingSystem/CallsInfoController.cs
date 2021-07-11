using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Services;
using Acorna.Core.Sheard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace billingSystem.Controllers.Project.BillingSystem
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CallsInfoController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public CallsInfoController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpPost]
        [Route("GetCallDetails")]
        public IActionResult GetCallDetails(CallsInfoFilterModel model)
        {
            try
            {
                var list = _unitOfWorkService.CallDetailsViewService.GetCallDetails(model);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
         
        [HttpPost]
        [Route("GetCallSummary")]
        public IActionResult GetCallSummary(CallsInfoFilterModel model)
        {
            try
            {
                var list = _unitOfWorkService.CallDetailsViewService.GetCallSummary(model);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("GetCallFinance")]
        public IActionResult GetCallFinance(CallsInfoFilterModel model)
        {
            try
            {
                var list = _unitOfWorkService.CallDetailsViewService.GetCallFinance(model);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
