using Acorna.Core.Entity.Notification;
using Acorna.Core.Models.Notification;
using Acorna.Core.Repository.Notification;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Acorna.Repository.Repository.Notification
{
    internal class NotificationRepository : Repository<NotificationItem>, INotificationRepository
    {
        private readonly IDbFactory _dbFactory;
        private readonly IMapper _mapper;

        internal NotificationRepository(IDbFactory dbFactory, IMapper mapper) : base(dbFactory)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public int AddNotificationItem(NotificationItemModel model)
        {
            NotificationItem notificationItem = _mapper.Map<NotificationItem>(model);
            List<NotificationTemplateTranslation> notificationTemplateTranslations = GetNotificationTemplateTranslation(model.NotificationTypeId);

            notificationTemplateTranslations.ForEach((NotificationTemplateTranslation notificationTemplateTranslation) =>
            {
                notificationItem.NotificationItemTranslations.Add(new NotificationItemTranslation
                {
                    LanguageCode = notificationTemplateTranslation.LanguageCode,
                    LanguageId = notificationTemplateTranslation.LanguageId,
                    MessageText = notificationTemplateTranslation.Name,
                });
            });
            return Insert(notificationItem);
        }

        public List<NotificationTemplateTranslation> GetNotificationTemplateTranslation(int notificationTypeId)
        {
            return _dbFactory.DataContext.NotificationTemplateTranslation.
                        AsNoTracking().Where(s => s.NotificationTypeId == notificationTypeId).ToList();
        }
    }
}
