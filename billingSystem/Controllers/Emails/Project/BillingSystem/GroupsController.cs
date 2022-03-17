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
    public class GroupsController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public GroupsController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetAllGroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                return Ok(await _unitOfWorkService.GroupService.GetAllGroups());
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
                return Ok(await _unitOfWorkService.GroupService.GetGroups(pageIndex, pageSize));
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
                return Ok(await _unitOfWorkService.GroupService.GetGroupId(groupId));
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
                return Ok(_unitOfWorkService.GroupService.AddGroup(groupModel));
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
                return Ok(_unitOfWorkService.GroupService.UpdateGroup(groupModel));
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
                return Ok(_unitOfWorkService.GroupService.DeleteGroup(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetGroupsByUser")]
        public async Task<IActionResult> GetAllGroupsByUser()
        {
            try
            {
                var list = await _unitOfWorkService.GroupService.GetGroupsByUserRole(CurrentUserId, CurrentUserRole);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
