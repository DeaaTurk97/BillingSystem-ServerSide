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
    public class BillsSummaryController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public BillsSummaryController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetBillsSummary")]
        public async Task<IActionResult> GetBillsSummary(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _unitOfWorkService.BillsSummaryService.GetBillsSummary(pageIndex, pageSize, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
