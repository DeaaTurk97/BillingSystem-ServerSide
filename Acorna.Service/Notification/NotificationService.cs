using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Acorna.Core.Entity.Notification;
using Acorna.Core.IServices.Notification;
using Acorna.Core.Models.Notification;
using Acorna.Core.Repository;

namespace Acorna.Service.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _imapper;
        private readonly IRepository<NotificationItem> _notificationItemRepository;

        public NotificationService(IMapper imapper
            , IRepository<NotificationItem> notificationItemRepository)
        {
            _imapper = imapper;
            _notificationItemRepository = notificationItemRepository;
        }

        public List<NotificationItemModel> GetAllNotificationItems()
        {
            try
            {
                List<NotificationItem> notificationItems = _notificationItemRepository.GetAll();
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
                List<NotificationItem> notificationItems = _notificationItemRepository
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
                List<NotificationItem> notificationItems = _notificationItemRepository
                    .GetWhere(s => s.IsRead == false).ToList();

                return _imapper.Map<List<NotificationItemModel>>(notificationItems);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public IEnumerable<NotificationItemModel> GetAllUnreadNotificationItems(int userId)
        {
            try
            {
                IEnumerable<NotificationItem> notificationItems = _notificationItemRepository
                    .GetWhere(s => s.IsRead == false & s.RecipientId == userId);

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
                NotificationItem notificationItem = _notificationItemRepository.GetSingle(id);
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
                NotificationItem notificationItem = _imapper.Map<NotificationItem>(model);
                int id = _notificationItemRepository.Insert(notificationItem);

                return id;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public bool ReadNotificationItem(int notificationId)
        {
            try
            {
                NotificationItem notificationItem = _notificationItemRepository.GetSingle(notificationId);
                notificationItem.IsRead = true;
                return _notificationItemRepository.Update(notificationItem);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool UpdateReadNotificationsItems(int receiverId, int senderId, int notificationTypeId)
        {
            try
            {
                List<NotificationItem> notificationItems = _notificationItemRepository
                    .GetWhere(s => s.IsRead == false && s.CreatedBy == senderId &&
                                   s.RecipientId == receiverId && s.NotificationTypeId == notificationTypeId).ToList();

                notificationItems.ForEach(read => read.IsRead = true);

                return _notificationItemRepository.UpdateRange(notificationItems);
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
                NotificationItem notificationItem = _notificationItemRepository.GetSingle(id);
                return _notificationItemRepository.Delete(notificationItem);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
