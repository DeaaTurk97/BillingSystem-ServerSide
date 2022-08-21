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
    public class BillsController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public BillsController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpPost]
        [Route("UploadMTNBills")]
        public IActionResult UploadMTNBills(List<DocumentModel> filesUploaded)
        {
            try
            {
                return Ok(_unitOfWorkService.BillService.UploadMTNBills(filesUploaded, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("UploadSyriaTelBills")]
        public IActionResult UploadSyriaTelBills(List<DocumentModel> filesUploaded)
        {
            try
            {
                return Ok(_unitOfWorkService.BillService.UploadSyriaTelBills(filesUploaded, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("UploadCallsAndRoamingLebanon")]
        public IActionResult UploadCallsAndRoamingLebanon(List<DocumentModel> filesUploaded, string billType)
        {
           
                return Ok(_unitOfWorkService.BillService.UploadCallsAndRoamingLebanon(filesUploaded, billType, CurrentUserId));
           
        }

        [HttpPost]
        [Route("UploadDataRoamingLebanon")]
        public IActionResult UploadDataRoamingLebanon(List<DocumentModel> filesUploaded)
        {
            try
            {
                return Ok(_unitOfWorkService.BillService.UploadDataRoamingLebanon(filesUploaded, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("UploadDataLebanon")]
        public IActionResult UploadDataLebanon(List<DocumentModel> filesUploaded)
        {
            try
            {
                return Ok(_unitOfWorkService.BillService.UploadDataLebanon(filesUploaded, CurrentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("ReminderUsersAddedBills")]
        public async Task<IActionResult> ReminderUsersAddedBills()
        {
            try
            {
                return Ok(await _unitOfWorkService.BillService.ReminderUsersAddedBills());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
