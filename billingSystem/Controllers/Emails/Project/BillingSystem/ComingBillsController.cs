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
    public class ComingBillsController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public ComingBillsController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetComingBills")]
        public async Task<IActionResult> GetComingBills(int pageIndex = 1, int pageSize = 10, int statusBill = 1)
        {
            try
            {
                return Ok(await _unitOfWorkService.ComingBillsService.GetComingBills(pageIndex, pageSize, statusBill, CurrentUserId, CurrentUserRole));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("ApproveBills")]
        public IActionResult ApproveBills(List<int> billsId)
        {
            try
            {
                return Ok(_unitOfWorkService.ComingBillsService.ApproveBills(billsId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("InprogressBills")]
        public IActionResult InprogressBills(List<int> phoneNumberId)
        {
            try
            {
                return Ok(_unitOfWorkService.ComingBillsService.InprogressBills(phoneNumberId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("RejectBills")]
        public IActionResult RejectBills(List<int> phoneNumberId)
        {
            try
            {
                return Ok(_unitOfWorkService.ComingBillsService.RejectBills(phoneNumberId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("PayBills")]
        public IActionResult PayBills(List<int> phoneNumberId)
        {
            try
            {
                return Ok(_unitOfWorkService.ComingBillsService.PayBills(phoneNumberId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
