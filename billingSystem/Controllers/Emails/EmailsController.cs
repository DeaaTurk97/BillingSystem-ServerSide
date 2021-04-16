using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acorna.Controllers.Base;
using Acorna.Core.Models.Email;
using Acorna.Core.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                EmailModel emailModel = new EmailModel();
                return Ok(await _emailService.SendTestEmail());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
