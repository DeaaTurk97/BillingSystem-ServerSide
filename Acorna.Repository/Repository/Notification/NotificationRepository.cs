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

        public async Task<List<string>> SendNotificationAddedBills()
        {
            try
            {
                List<string> usersIDNewUplodedBills = new List<string>();

                var allocatedUsersService = await (from aus in _dbFactory.DataContext.AllocatedUsersService
                                                   join u in _dbFactory.DataContext.ServiceUsed on aus.ServiceUsedId equals u.Id
                                                   group u by new { aus.UserId } into gServicesPrices
                                                   select new
                                                   {
                                                       UserId = gServicesPrices.Key.UserId,
                                                       SumServicesPrices = gServicesPrices.Sum(s => s.ServicePrice).Value
                                                   }).ToListAsync();

                var bills = await (from b in _dbFactory.DataContext.Bill
                                   join bd in _dbFactory.DataContext.BillDetails on b.Id equals bd.BillId
                                   where b.SubmittedByUser == false
                                   group bd by new { b.Id, b.UserId } into gSumPrice
                                   select new
                                   {
                                       BillId = gSumPrice.Key.Id,
                                       UserId = gSumPrice.Key.UserId,
                                       SumNetPrice = gSumPrice.Sum(s => s.CallNetPrice),
                                   }).ToListAsync();


                var billsGreaterThanServicesPrices = (from aus in allocatedUsersService
                                                      join b in bills on aus.UserId equals b.UserId
                                                      where b.SumNetPrice > 0 // aus.SumServicesPrices
                                                      select new
                                                      {
                                                          UserId = aus.UserId,
                                                      }).ToList();


                foreach (var bill in billsGreaterThanServicesPrices)
                {
                    if (!usersIDNewUplodedBills.Contains(bill.UserId.ToString()))
                    {
                        usersIDNewUplodedBills.Add(bill.UserId.ToString());
                    }
                }

                return usersIDNewUplodedBills;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                                                                          && n.RecipientRoleId == (int)enumRoleName
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
