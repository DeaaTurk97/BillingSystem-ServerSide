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
    public class OperatorsController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public OperatorsController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllOperators")]
        public async Task<IActionResult> GetAllOperators()
        {
            try
            {
                return Ok(await _unitOfWorkService.OperatorService.GetAllOperators());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetOperators")]
        public async Task<IActionResult> GetOperators(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _unitOfWorkService.OperatorService.GetOperators(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetOperatorId")]
        public async Task<IActionResult> GetOperatorId(int groupId)
        {
            try
            {
                return Ok(await _unitOfWorkService.OperatorService.GetOperatorId(groupId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddOperator")]
        public IActionResult AddOperator(OperatorModel operatorModel)
        {
            try
            {
                return Ok(_unitOfWorkService.OperatorService.AddOperator(operatorModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateOperator")]
        public IActionResult UpdateOperator(OperatorModel operatorModel)
        {
            try
            {
                return Ok(_unitOfWorkService.OperatorService.UpdateOperator(operatorModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeleteOperator")]
        public IActionResult DeleteOperator(int id)
        {
            try
            {
                return Ok(_unitOfWorkService.OperatorService.DeleteOperator(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
