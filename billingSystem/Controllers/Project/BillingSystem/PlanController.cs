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
    public class PlanController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public PlanController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllPlans")]
        public async Task<IActionResult> GetAllPlans()
        {
            try
            {
                return Ok(await _unitOfWorkService.PlanService.GetAllPlans());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetPlan")]
        public async Task<IActionResult> GetPlan(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _unitOfWorkService.PlanService.GetPlan(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpGet]
        [Route("GetPlanId")]
        public async Task<IActionResult> GetPlanId(int planId)
        {
            try
            {
                return Ok(await _unitOfWorkService.PlanService.GetPlanId(planId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddPlan")]
        public IActionResult AddPlan(PlanModel planModel)
        {
            try
            {
                return Ok(_unitOfWorkService.PlanService.AddPlan(planModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut]
        [Route("UpdatePlan")]
        public async Task<IActionResult> UpdatePlan([FromBody] PlanModel planModel)
        {
            try
            {
                return Ok(_unitOfWorkService.PlanService.UpdatePlan(planModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        [HttpDelete]
        [Route("DeletePlan")]
        public IActionResult DeletePlan(int id)
        {
            try
            {
                return Ok(_unitOfWorkService.PlanService.DeletePlan(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
