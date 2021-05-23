using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Acorna.Controllers.Project
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public JobsController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            try
            {
                return Ok(await _unitOfWorkService.JobService.GetAllJobs());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetJobs")]
        public async Task<IActionResult> GetJobs(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _unitOfWorkService.JobService.GetJobs(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetJobId")]
        public async Task<IActionResult> GetJobId(int jobId)
        {
            try
            {
                return Ok(await _unitOfWorkService.JobService.GetJobId(jobId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddJob")]
        public IActionResult AddJob(JobModel jobModel)
        {
            try
            {
                return Ok(_unitOfWorkService.JobService.AddJob(jobModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateJob")]
        public IActionResult UpdateJob(JobModel jobModel)
        {
            try
            {
                return Ok(_unitOfWorkService.JobService.UpdateJob(jobModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeleteJob")]
        public IActionResult DeleteJob(int id)
        {
            try
            {
                return Ok(_unitOfWorkService.JobService.DeleteJob(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

