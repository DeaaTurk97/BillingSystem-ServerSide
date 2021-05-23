using Acorna.Controllers.Base;
using Acorna.Core.Models.Notification;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [Route("GetAllUnreadNotification")]
        public IActionResult GetAllUnreadNotification()
        {
            try
            {
                return Ok(_unitOfWorkService.NotificationService.GetAllUnreadNotificationItems(CurrentUserId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UpdateAllReadNotifications")]
        public IActionResult UpdateAllReadNotifications(NotificationItemModel notificationItemModel)
        {
            try
            {
                return Ok(_unitOfWorkService.NotificationService.UpdateReadNotificationsItems(CurrentUserId, notificationItemModel.CreatedBy, notificationItemModel.NotificationTypeId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
