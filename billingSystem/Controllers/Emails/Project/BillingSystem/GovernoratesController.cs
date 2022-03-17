using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem;
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
    public class GovernoratesController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public GovernoratesController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllGovernorates")]
        public async Task<IActionResult> GetAllGovernorates()
        {
            try
            {
                return Ok(await _unitOfWorkService.GovernorateService.GetAllGovernorates());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetGovernorates")]
        public async Task<IActionResult> GetGovernorates(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _unitOfWorkService.GovernorateService.GetGovernorates(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetGovernorateId")]
        public async Task<IActionResult> GetGovernorateId(int countryId)
        {
            try
            {
                return Ok(await _unitOfWorkService.GovernorateService.GetGovernorateId(countryId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddGovernorate")]
        public IActionResult AddGovernorate(GovernorateModel governorateModel)
        {
            try
            {
                return Ok(_unitOfWorkService.GovernorateService.AddGovernorate(governorateModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateGovernorate")]
        public IActionResult UpdateGovernorate(GovernorateModel countryModel)
        {
            try
            {
                return Ok(_unitOfWorkService.GovernorateService.UpdateGovernorate(countryModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeleteGovernorate")]
        public IActionResult DeleteGovernorate(int id)
        {
            try
            {
                return Ok(_unitOfWorkService.GovernorateService.DeleteGovernorate(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
