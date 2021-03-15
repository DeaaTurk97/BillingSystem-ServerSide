using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Controllers.Base;
using Acorna.Core.IServices.Project;
using Acorna.Core.Models.Project;

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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<JobModel> jobModels = await _jobService.GetAllAsync();
                return Ok(jobModels);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _jobService.GetAllAsync(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] JobModel jobModel)
        {
            try
            {
                return Ok(_jobService.Insert(jobModel));
            }
            catch (Exception)
            {
                return BadRequest("false");
            }
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] JobModel jobModel)
        {
            try
            {
                if (id != 0)
                    _jobService.Update(jobModel);
            }
            catch (Exception)
            {
                return BadRequest("false");
            }

            return Ok("true");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _jobService.Delete(id);
            }
            catch (Exception)
            {
                BadRequest("false");
            }

            return Ok("true");
        }
    }
}

