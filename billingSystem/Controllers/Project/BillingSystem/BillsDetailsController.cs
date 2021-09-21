using Acorna.Controllers.Base;
using Acorna.Core.DTOs.billingSystem;
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
    public class BillsDetailsController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public BillsDetailsController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllUndefinedNumbers")]
        public async Task<IActionResult> GetAllUndefinedNumbers(int billId)
        {
            try
            {
                return Ok(await _unitOfWorkService.BillsDetailsService.GetAllUndefinedNumbers(billId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("DefinitionNewNumbers")]
        public IActionResult DefinitionNewNumbers(List<UnDefinedNumbersDTO> phoneNumbers, string billId)
        {
            try
            {
                return Ok(_unitOfWorkService.BillsDetailsService.DefinitionNewNumbers(phoneNumbers, billId, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("ServicesSubmitted")]
        public IActionResult ServicesSubmitted(List<ServicesNeedApprovedDTO> servicesNeedApproved, string billId)
        {
            try
            {
                return Ok(_unitOfWorkService.BillsDetailsService.ServicesSubmitted(servicesNeedApproved, billId, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateSubmitBill")]
        public IActionResult UpdateSubmitBill(string billId)
        {
            try
            {
                return Ok(_unitOfWorkService.BillsDetailsService.UpdateSubmitBill(Convert.ToInt32(billId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetServicesNeedApproval")]
        public async Task<IActionResult> GetServicesNeedApproval(int billId)
        {
            try
            {
                return Ok(await _unitOfWorkService.BillsDetailsService.GetServicesNeedApproval(billId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
