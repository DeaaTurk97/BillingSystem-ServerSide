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
    public class HistoryController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public HistoryController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllHistory")]
        public async Task<IActionResult> GetAllHistory()
        {
            try
            {
                return Ok(await _unitOfWorkService.HistoryService.GetAllHistory());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetHistory")]
        public async Task<IActionResult> GetHistory(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _unitOfWorkService.HistoryService.GetHistory(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpGet]
        [Route("GetHistoryId")]
        public async Task<IActionResult> GetHistoryId(int historyId)
        {
            try
            {
                return Ok(await _unitOfWorkService.HistoryService.GetHistoryId(historyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddHistory")]
        public IActionResult AddHistory(HistoryModel historyModel)
        {
            try
            {
                return Ok(_unitOfWorkService.HistoryService.AddHistory(historyModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut]
        [Route("UpdateHistory")]
        public async Task<IActionResult> UpdateHistory([FromBody] HistoryModel historyModel)
        {
            try
            {
                return Ok(_unitOfWorkService.HistoryService.UpdateHistory(historyModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        [HttpDelete]
        [Route("DeleteHistory")]
        public IActionResult DeleteHistory(int id)
        {
            try
            {
                return Ok(_unitOfWorkService.HistoryService.DeleteHistory(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

