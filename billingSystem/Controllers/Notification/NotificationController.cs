using Acorna.Controllers.Base;
using Acorna.Core.Models.Notification;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Acorna.Controllers.Notification
{

    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public NotificationController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        [Route("GetUnreadNotification")]
        public IActionResult GetUnreadNotification()
        {
            try
            {
                return Ok(_unitOfWorkService.NotificationService.GetUnreadNotification(CurrentUserId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UpdateReadNewNotification")]
        public IActionResult UpdateReadNewNotification(NotificationItemModel notificationItemModel)
        {
            try
            {
                return Ok(_unitOfWorkService.NotificationService.UpdateReadNewNotification(CurrentUserId, notificationItemModel.CreatedBy, notificationItemModel.NotificationTypeId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetNewNumbersAndBillsByRoleId")]
        public async Task<IActionResult> GetNewNumbersAndBillsByRoleId()
        {
            try
            {
                return Ok(await _unitOfWorkService.NotificationService.GetNewNumbersAndBillsByRoleId(CurrentUserRole));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UpdateReadNewNumbersAndBills")]
        public IActionResult UpdateReadNewNumbersAndBills(NotificationItemModel notificationItemModel)
        {
            try
            {
                return Ok(_unitOfWorkService.NotificationService.UpdateReadNewNumbersAndBills(CurrentUserRole, notificationItemModel.NotificationTypeId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
