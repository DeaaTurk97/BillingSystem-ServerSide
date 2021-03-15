using Microsoft.AspNetCore.Mvc;
using System;
using Acorna.Controllers.Base;
using Acorna.Core.IServices.Notification;
using Acorna.Core.Models.Notification;

namespace Acorna.Controllers.Notification
{

    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : TeamControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("GetAllUnreadNotification")]
        public IActionResult GetAllUnreadNotification()
        {
            try
            {
                return Ok(_notificationService.GetAllUnreadNotificationItems(CurrentUserId));
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
                return Ok(_notificationService.UpdateReadNotificationsItems(CurrentUserId, notificationItemModel.CreatedBy, notificationItemModel.NotificationTypeId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
