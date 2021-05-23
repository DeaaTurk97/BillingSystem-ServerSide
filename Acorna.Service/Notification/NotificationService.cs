using Acorna.Core.Entity.Notification;
using Acorna.Core.IServices.Notification;
using Acorna.Core.Models.Notification;
using Acorna.Core.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
                List<NotificationItem> notificationItems = _unitOfWork.NotificationRepository.GetAll();
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
                List<NotificationItem> notificationItems = _unitOfWork.NotificationRepository
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
                List<NotificationItem> notificationItems = _unitOfWork.NotificationRepository
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
                IEnumerable<NotificationItem> notificationItems = _unitOfWork.NotificationRepository
                    .GetAllIncludingWithPredicate(
                    new Expression<Func<NotificationItem, object>>[] { x => x.NotificationItemTranslations }
                    , new Expression<Func<NotificationItem, bool>>[] { s => s.IsRead == false & s.RecipientId == userId });

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
                NotificationItem notificationItem = _unitOfWork.NotificationRepository.GetSingle(id);
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
                NotificationItem notificationItem = _unitOfWork.NotificationRepository.GetSingle(notificationId);
                notificationItem.IsRead = true;
                return _unitOfWork.NotificationRepository.Update(notificationItem);
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
                List<NotificationItem> notificationItems = _unitOfWork.NotificationRepository
                    .GetWhere(s => s.IsRead == false && s.CreatedBy == senderId &&
                                   (s.RecipientId == receiverId) && s.NotificationTypeId == notificationTypeId).ToList();

                notificationItems.ForEach(read => read.IsRead = true);
                _unitOfWork.NotificationRepository.UpdateRange(notificationItems);

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
                NotificationItem notificationItem = _unitOfWork.NotificationRepository.GetSingle(id);
                return _unitOfWork.NotificationRepository.Delete(notificationItem);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
