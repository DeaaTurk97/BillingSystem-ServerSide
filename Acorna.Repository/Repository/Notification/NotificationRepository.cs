using Acorna.Core.Entity.Notification;
using Acorna.Core.Models.Notification;
using Acorna.Core.Repository.Notification;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Repository.Repository.Notification
{
    internal class NotificationRepository : Repository<Notifications>, INotificationRepository
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
            try
            {
                Notifications notificationItem = _mapper.Map<Notifications>(model);
                notificationItem.NotificationsDetails = new List<NotificationsDetails>();

                notificationItem.NotificationsDetails.Add(new NotificationsDetails
                {
                    MessageText = model.MessageText,
                });

                Insert(notificationItem);
                _dbFactory.DataContext.SaveChanges();

                return notificationItem.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<NotificationItemModel>> GetNewNumbersAndBillsByRoleId(string currentUserRole)
        {
            try
            {
                RolesType enumRoleName = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);

                IEnumerable<NotificationItemModel> notifications = await (from n in _dbFactory.DataContext.Notifications
                                                                          join nd in _dbFactory.DataContext.NotificationsDetails
                                                                          on n.Id equals nd.NotificationsId
                                                                          where n.IsRead == false && n.Deleted == false 
                                                                          &&  n.RecipientRoleId == (int)enumRoleName
                                                                          select new NotificationItemModel
                                                                          {
                                                                              Id = n.Id,
                                                                              MessageText = nd.MessageText,
                                                                              NotificationTypeId = n.NotificationTypeId,
                                                                              ReferenceMassageId = n.ReferenceMassageId,
                                                                          }).Distinct().ToListAsync();


                return notifications;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
