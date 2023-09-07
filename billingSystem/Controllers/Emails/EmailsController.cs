using Acorna.Controllers.Base;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace billingSystem.Controllers.Emails
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public EmailsController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpPost]
        [Route("SendTestEmail")]
        public async Task<IActionResult> SendTestEmail()
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.SendTestEmail());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("ImportBillEmail")]
        public async Task<IActionResult> ImportBillEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.ImportBillEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("ReminderIdentifyNewNumbersEmail")]
        public async Task<IActionResult> ReminderIdentifyNewNumbersEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.ReminderIdentifyNewNumbersEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("RejectNumberEmail")]
        public async Task<IActionResult> RejectNumberEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.RejectNumberEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("PaidEmail")]
        public async Task<IActionResult> PaidEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.PaidEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("PaidEmail")]
        public async Task<IActionResult> ApprovedEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.ApprovedEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("UnpaidEmail")]
        public async Task<IActionResult> UnpaidEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.UnpaidEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("SubmittedBillEmail")]
        public async Task<IActionResult> SubmittedBillEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.SubmittedBillEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("ReminderTotalDueEmail")]
        public async Task<IActionResult> ReminderTotalDueEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.ReminderTotalDueEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("ReminderStartPeriodSubmittBillEmail")]
        public async Task<IActionResult> ReminderStartPeriodSubmittBillEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.ReminderStartPeriodSubmittBillEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("ReminderEndPeriodSubmittBillEmail")]
        public async Task<IActionResult> ReminderEndPeriodSubmittBillEmail(string toEmail)
        {
            try
            {
                return Ok(await _unitOfWorkService.EmailService.ReminderEndPeriodSubmittBillEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
