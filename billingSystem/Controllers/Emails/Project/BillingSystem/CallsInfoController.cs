using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace billingSystem.Controllers.Project.BillingSystem
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CallsInfoController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public CallsInfoController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpPost]
        [Route("GetCallDetails")]
        public IActionResult GetCallDetails(CallsInfoFilterModel model)
        {
            try
            {
                this.PrepareCallsInfoFilterModel(model);

                var list = _unitOfWorkService.CallDetailsViewService.GetCallDetails(model);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("GetCallSummary")]
        public IActionResult GetCallSummary(CallsInfoFilterModel model)
        {
            try
            {
                this.PrepareCallsInfoFilterModel(model);
                var list = _unitOfWorkService.CallDetailsViewService.GetCallSummary(model);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("GetCallFinance")]
        public IActionResult GetCallFinance(CallsInfoFilterModel model)
        {
            try
            {
                this.PrepareCallsInfoFilterModel(model);

                var list = _unitOfWorkService.CallDetailsViewService.GetCallFinance(model);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        

        private void PrepareCallsInfoFilterModel(CallsInfoFilterModel model)
        {
            model.CurrentUserId = CurrentUserId;
            model.GroupId = model.GroupId != null && model.GroupId.Value > 0 ? model.GroupId.Value : (int?)null;
            model.UserId = model.UserId != null && model.UserId.Value > 0 ? model.UserId.Value : (int?)null;
            model.ServiceUsedId = model.ServiceUsedId != null && model.ServiceUsedId.Value > 0 ? model.ServiceUsedId.Value : (int?)null;
            model.CountryId = model.CountryId != null && model.CountryId.Value > 0 ? model.CountryId.Value : (int?)null;
            model.CountryIdExclude = model.CountryIdExclude != null && model.CountryIdExclude.Value > 0 ? model.CountryIdExclude.Value : (int?)null;
            model.TypePhoneNumberId = model.TypePhoneNumberId != null && model.TypePhoneNumberId.Value > 0 ? model.TypePhoneNumberId.Value : (int?)null;
            model.IsSubmitted = model.IsSubmitted != null && model.IsSubmitted.Value != false ? model.IsSubmitted.Value : (bool?)null;
        }
    }
}
