using Acorna.Controllers.Base;
using Acorna.Core.IServices.Project;
using Acorna.Core.Models.Project.BillingSystem;
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
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        [Route("GetAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            try
            {
                return Ok(await _jobService.GetAllJobs());
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
                return Ok(await _jobService.GetJobs(pageIndex, pageSize));
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
                return Ok(await _jobService.GetJobId(jobId));
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
                return Ok(_jobService.AddJob(jobModel));
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
                return Ok(_jobService.UpdateJob(jobModel));
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
                return Ok(_jobService.DeleteJob(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

