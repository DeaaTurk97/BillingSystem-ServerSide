using Acorna.Core.Entity.Notification;
using Acorna.Core.IServices.Notification;
using Acorna.Core.Models.Notification;
using Acorna.Core.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Service.Notification
{
    internal class NotificationService : INotificationService
    {
        private readonly IMapper _imapper;
        private readonly IUnitOfWork _unitOfWork;

        internal NotificationService(IUnitOfWork unitOfWork, IMapper imapper)
        {
            _unitOfWork = unitOfWork;
            _imapper = imapper;
        }

        public List<NotificationItemModel> GetAllNotificationItems()
        {
            try
            {
                List<Notifications> notificationItems = _unitOfWork.NotificationRepository.GetAll();
                return _imapper.Map<List<NotificationItemModel>>(notificationItems);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public List<NotificationItemModel> GetAllNotificationItems(int userId)
        {
            try
            {
                List<Notifications> notificationItems = _unitOfWork.NotificationRepository
                    .GetWhere(s => s.RecipientId == userId).ToList();

                return _imapper.Map<List<NotificationItemModel>>(notificationItems);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public List<NotificationItemModel> GetAllUnreadNotificationItems()
        {
            try
            {
                List<Notifications> notificationItems = _unitOfWork.NotificationRepository
                    .GetWhere(s => s.IsRead == false).ToList();

                return _imapper.Map<List<NotificationItemModel>>(notificationItems);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public IEnumerable<NotificationItemModel> GetUnreadNotification(int userId)
        {
            try
            {
                IEnumerable<Notifications> notificationItems = _unitOfWork.NotificationRepository
                    .GetAllIncludingWithPredicate(
                    new Expression<Func<Notifications, object>>[] { x => x.NotificationsDetails }
                    , new Expression<Func<Notifications, bool>>[] { s => s.IsRead == false & s.RecipientId == userId });

                return _imapper.Map<List<NotificationItemModel>>(notificationItems);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public NotificationItemModel GetNotificationItem(int id)
        {
            try
            {
                Notifications notificationItem = _unitOfWork.NotificationRepository.GetSingle(id);
                return _imapper.Map<NotificationItemModel>(notificationItem);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public int AddNotificationItem(NotificationItemModel model)
        {
            try
            {
                int notificationId = _unitOfWork.NotificationRepository.AddNotificationItem(model);
                _unitOfWork.SaveChanges();
                return notificationId;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool ReadNotificationItem(int notificationId)
        {
            try
            {
                Notifications notificationItem = _unitOfWork.NotificationRepository.GetSingle(notificationId);
                notificationItem.IsRead = true;
                return _unitOfWork.NotificationRepository.Update(notificationItem);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool UpdateReadNewNotification(int receiverId, int senderId, int notificationTypeId, int notificationId)
        {
            try
            {
                if (notificationTypeId == 50)
                {
                    List<Notifications> notificationItems = _unitOfWork.NotificationRepository
                        .GetWhere(s => s.IsRead == false && s.CreatedBy == senderId &&
                                       (s.RecipientId == receiverId) && s.NotificationTypeId == 50).ToList();

                    notificationItems.ForEach(read => read.IsRead = true);
                    _unitOfWork.NotificationRepository.UpdateRange(notificationItems);
                }
                else
                {
                    var notification = _unitOfWork.NotificationRepository.FirstOrDefault(n => n.Id == notificationId);

                    notification.IsRead = true;

                    _unitOfWork.NotificationRepository.Update(notification);
                }

                //List<Notifications> notificationItems = _unitOfWork.NotificationRepository
                //    .GetWhere(s => s.IsRead == false && s.CreatedBy == senderId &&
                //                   (s.RecipientId == receiverId) && s.NotificationTypeId == notificationTypeId).ToList();

                //notificationItems.ForEach(read => read.IsRead = true);
                //_unitOfWork.NotificationRepository.UpdateRange(notificationItems);
                return _unitOfWork.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteNotificationItem(int id)
        {
            try
            {
                Notifications notificationItem = _unitOfWork.NotificationRepository.GetSingle(id);
                return _unitOfWork.NotificationRepository.Delete(notificationItem);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<NotificationItemModel>> GetNewNumbersAndBillsByRoleId(string currentUserRole)
        {
            try
            {
                return await _unitOfWork.NotificationRepository.GetNewNumbersAndBillsByRoleId(currentUserRole);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool UpdateReadNewNumbersAndBills(string currentUserRole, int notificationTypeId)
        {
            try
            {
                RolesType enumRoleName = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);

                List<Notifications> notifications = _unitOfWork.GetRepository<Notifications>()
                    .GetWhere(s => s.IsRead == false &&
                                   (s.RecipientRoleId == (int)enumRoleName) && s.NotificationTypeId == notificationTypeId).ToList();

                notifications.ForEach(read => read.IsRead = true);

                _unitOfWork.GetRepository<Notifications>().UpdateRange(notifications);
                return _unitOfWork.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
