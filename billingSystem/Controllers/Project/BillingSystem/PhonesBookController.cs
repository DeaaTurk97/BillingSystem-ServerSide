using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Services.Project.BillingSystem;
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
    public class PhonesBookController : TeamControllerBase
    {
        private readonly IPhoneBookService _phoneBookService;

        public PhonesBookController(IPhoneBookService phoneBook)
        {
            _phoneBookService = phoneBook;
        }

        [HttpGet]
        [Route("GetAllPhonesBook")]
        public async Task<IActionResult> GetAllPhonesBook()
        {
            try
            {
                string st = CurrentUserRole;
                return Ok(await _phoneBookService.GetAllPhonesBook());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetPhonesBook")]
        public async Task<IActionResult> GetPhonesBook(int pageIndex = 1, int pageSize = 10)
        {
            PaginationRecord<PhoneBookModel> phoneBookModel = new PaginationRecord<PhoneBookModel>();

            try
            {
                return Ok(await _phoneBookService.GetPhonesBook(pageIndex, pageSize, CurrentUserId, CurrentUserRole));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetPhoneBookId")]
        public async Task<IActionResult> GetPhoneBookId(int phoneBookId)
        {
            try
            {
                return Ok(await _phoneBookService.GetPhoneBookId(phoneBookId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddPhoneBook")]
        public IActionResult AddPhoneBook(PhoneBookModel phoneBookModel)
        {
            try
            {
                return Ok(_phoneBookService.AddPhoneBook(phoneBookModel, CurrentUserId, CurrentUserRole));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("IsNumberAdded")]
        public async Task<IActionResult> IsNumberAdded(string phoneNumber)
        {
            try
            {
                return Ok( await _phoneBookService.IsNumberAdded(phoneNumber, CurrentUserId, CurrentUserRole));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdatePhoneBook")]
        public IActionResult UpdatePhoneBook(PhoneBookModel phoneBookModel)
        {
            try
            {
                return Ok(_phoneBookService.UpdatePhoneBook(phoneBookModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeletePhoneBook")]
        public IActionResult DeletePhoneBook(int id)
        {
            try
            {
                return Ok(_phoneBookService.DeletePhoneBook(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
