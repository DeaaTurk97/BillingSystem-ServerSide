using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Services.Project.BillingSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace billingSystem.Controllers.Project.BillingSystem
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : TeamControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Route("GetAllGroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                return Ok(await _groupService.GetAllGroups());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetGroups")]
        public async Task<IActionResult> GetGroups(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _groupService.GetGroups(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetGroupId")]
        public async Task<IActionResult> GetGroupId(int groupId)
        {
            try
            {
                return Ok(await _groupService.GetGroupId(groupId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddGroup")]
        public IActionResult AddGroup(GroupModel groupModel)
        {
            try
            {
                return Ok(_groupService.AddGroup(groupModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateGroup")]
        public IActionResult UpdateGroup(GroupModel groupModel)
        {
            try
            {
                return Ok(_groupService.UpdateGroup(groupModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeleteGroup")]
        public IActionResult DeleteGroup(int id)
        {
            try
            {
                return Ok(_groupService.DeleteGroup(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
