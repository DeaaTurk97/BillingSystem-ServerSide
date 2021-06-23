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
    public class ReportController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public ReportController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetCallDetailsReport")]
        public async Task<IActionResult> GetCallDetailsReport()
        {
            try
            {
                string st = CurrentUserRole;
                var list = await _unitOfWorkService.CallDetailsReportService.GetReport(new CallDetailsReportModel());
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
