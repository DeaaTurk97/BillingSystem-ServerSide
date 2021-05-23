using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Services;
using Acorna.Core.Sheard;
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
    public class ComingNumbersController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public ComingNumbersController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetComingNumbers")]
        public async Task<IActionResult> GetPhonesBook(int pageIndex = 1, int pageSize = 10, int statusNumber = 1)
        {
            PaginationRecord<PhoneBookModel> phoneBookModel = new PaginationRecord<PhoneBookModel>();

            try
            {
                return Ok(await _unitOfWorkService.IncomingNumbersService.GetComingNumbers(pageIndex, pageSize, statusNumber, CurrentUserId, CurrentUserRole));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("ApprovePhoneNumbers")]
        public IActionResult ApprovePhoneNumbers(List<int> phoneNumberId)
        {
            try
            {
                return Ok(_unitOfWorkService.IncomingNumbersService.ApprovePhoneNumbers(phoneNumberId, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("InprogressPhoneNumbers")]
        public IActionResult InprogressPhoneNumbers(List<int> phoneNumberId)
        {
            try
            {
                return Ok(_unitOfWorkService.IncomingNumbersService.InprogressPhoneNumbers(phoneNumberId, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut]
        [Route("RejectPhoneNumbers")]
        public IActionResult RejectPhoneNumbers(List<int> phoneNumberId)
        {
            try
            {
                return Ok(_unitOfWorkService.IncomingNumbersService.RejectPhoneNumbers(phoneNumberId, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
