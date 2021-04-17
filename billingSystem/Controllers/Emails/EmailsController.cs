using Acorna.Controllers.Base;
using Acorna.Core.Services.Email;
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
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("SendTestEmail")]
        public async Task<IActionResult> SendTestEmail()
        {
            try
            {
                return Ok(await _emailService.SendTestEmail());
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
                return Ok(await _emailService.ImportBillEmail(toEmail));
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
                return Ok(await _emailService.ReminderIdentifyNewNumbersEmail(toEmail));
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
                return Ok(await _emailService.RejectNumberEmail(toEmail));
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
                return Ok(await _emailService.PaidEmail(toEmail));
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
                return Ok(await _emailService.UnpaidEmail(toEmail));
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
                return Ok(await _emailService.SubmittedBillEmail(toEmail));
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
                return Ok(await _emailService.ReminderTotalDueEmail(toEmail));
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
                return Ok(await _emailService.ReminderStartPeriodSubmittBillEmail(toEmail));
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
                return Ok(await _emailService.ReminderEndPeriodSubmittBillEmail(toEmail));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
