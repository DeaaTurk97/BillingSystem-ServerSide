using Acorna.Core.DTOs;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Notification;
using Acorna.Core.Repository.ICustomRepsitory;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acorna.Repository.Repository.CustomRepository
{
    internal class BillRepository : Repository<Bill>, IBillsRepository
    {
        private readonly IDbFactory _dbFactory;
        private readonly IMapper _mapper;

        internal BillRepository(IDbFactory dbFactory, IMapper mapper) : base(dbFactory)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        //Tuple<string (users Ids) that have notification, List<NotificationItemModel> (newServicesAdded) , List<NotificationItemModel> (servicesRemoved), List<NotificationItemModel> (servicesGraterThanPlanServies)>
        public async Task<Tuple<List<string>, List<NotificationItemModel>, List<NotificationItemModel>, List<NotificationItemModel>>> GetbillsGreaterThanServicesPrices()
        {
            try
            {
                List<NotificationItemModel> newServicesAdded = new List<NotificationItemModel>();
                List<NotificationItemModel> servicesRemoved = new List<NotificationItemModel>();
                List<NotificationItemModel> servicesGraterThanPlanServies = new List<NotificationItemModel>();
                List<string> acceptedBills = new List<string>();
                List<string> userIds = new List<string>();

                //get all bills not submitted
                var bills = await (from b in _dbFactory.DataContext.Bill
                                   join usr in _dbFactory.DataContext.Users on b.UserId equals usr.Id
                                   where b.SubmittedByUser == false && (usr.PlanId != 0 || usr.PlanId != null)
                                   select new
                                   {
                                       BillId = b.Id,
                                       UserId = usr.Id,
                                       PlanId = usr.PlanId
                                   }).ToListAsync();

                //get all bills with sum services per user
                var userBillServices = await (from b in _dbFactory.DataContext.Bill
                                   join bd in _dbFactory.DataContext.BillDetails on b.Id equals bd.BillId
                                   where b.SubmittedByUser == false
                                   group bd by new { b.Id, b.UserId, bd.ServiceUsedId } into gServiceSumPrice
                                   select new
                                   {
                                       BillId = gServiceSumPrice.Key.Id,
                                       UserId = gServiceSumPrice.Key.UserId,
                                       ServiceId = gServiceSumPrice.Key.ServiceUsedId,
                                       SumServiceNetPrice = Convert.ToDouble(gServiceSumPrice.Sum(s => s.CallNetPrice)),
                                   }).ToListAsync();


                foreach (var bill in bills)
                {
                    //get 
                    var userBillService = userBillServices.Where(x => x.UserId == bill.UserId).ToList();
                    var planService = _dbFactory.DataContext.PlanService.Where(item => item.PlanId == bill.PlanId)
                                                                        .Select(item => new
                                                                        {
                                                                            ServiceUsedId = item.ServiceUsedId,
                                                                            ServicePrice = item.Limit
                                                                        })
                                                                        .ToList();

                    var planServiceCount = planService?.Count();
                    var userBillServiceCount = userBillService?.Count();

                    if (userBillServiceCount > planServiceCount)
                    {
                        //you have service excloude from user bills details service.
                        newServicesAdded.Add(new NotificationItemModel
                        {
                            MessageText = "NewServiceAdded",
                            IsRead = false,
                            Deleted = false,
                            RecipientId = bill.UserId,
                            ReferenceMassageId = bill.BillId,
                            NotificationTypeId = (int)SystemEnum.NotificationType.NewServiceAdded,
                            RecipientRoleId = 0
                        });

                        foreach (var billService in userBillService)
                        {
                            var newServiceAdded = planService.Where(r => r.ServiceUsedId == billService.ServiceId).FirstOrDefault();

                            if(newServiceAdded == null)
                            {
                                UpdateServiceNeedApproval(billService.BillId, billService.ServiceId);
                            }
                        }

                        userIds.Add(bill.UserId.ToString());
                    }
                    else if (planServiceCount > userBillServiceCount)
                    {
                        //you have new service added for user
                        servicesRemoved.Add(new NotificationItemModel 
                        {
                            MessageText = "ServiceRemoved",
                            IsRead = false,
                            Deleted = false,
                            RecipientId = bill.UserId,
                            ReferenceMassageId = bill.BillId,
                            NotificationTypeId = (int)SystemEnum.NotificationType.ServiceRemoved,
                            RecipientRoleId = 0
                        });

                        if (!userIds.Contains(bill.UserId.ToString()))
                        {
                            userIds.Add(bill.UserId.ToString());
                        }
                    }


                    foreach (var billService in userBillService)
                    {
                        //when paln service equal user bills details service
                        var limitPrice = planService.Find(f => f.ServiceUsedId == billService.ServiceId);

                        if (limitPrice != null && billService.SumServiceNetPrice > Convert.ToDouble(limitPrice))
                        {
                            //you have service in bill details grater than plan service
                            servicesGraterThanPlanServies.Add(new NotificationItemModel
                            {
                                MessageText = "ServicePriceGraterThanServicePlan",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = bill.UserId,
                                ReferenceMassageId = bill.BillId,
                                NotificationTypeId = (int)SystemEnum.NotificationType.ServicePriceGraterThanServicePlan,
                                RecipientRoleId = 0
                            });

                            UpdateServiceNeedApproval(billService.BillId, billService.ServiceId);

                            if (!userIds.Contains(bill.UserId.ToString()))
                            {
                                userIds.Add(bill.UserId.ToString());
                            }
                        }
                    }

                    //approve bill 
                    if (!userIds.Contains(bill.UserId.ToString()))
                    {
                        var userBill = _dbFactory.DataContext.Bill.Where(r => r.Id == bill.BillId).FirstOrDefault();

                        userBill.SubmittedByAdmin = true;
                        userBill.SubmittedByUser = true;
                        userBill.IsPaid = true;
                        userBill.SubmittedDate = DateTime.Now;

                        _dbFactory.DataContext.Bill.UpdateRange(userBill);
                        _dbFactory.DataContext.SaveChanges();
                    }
                }

                return Tuple.Create(userIds, newServicesAdded, servicesRemoved, servicesGraterThanPlanServies);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateServiceNeedApproval(int billId, int serviceId)
        {
            var billDetails = _dbFactory.DataContext.BillDetails.Where(r => r.BillId == billId && r.ServiceUsedId == serviceId).ToList();

            foreach (var billDetail in billDetails)
            {
                billDetail.IsServiceUsedNeedApproved = true;
            }

            _dbFactory.DataContext.BillDetails.UpdateRange(billDetails);
            _dbFactory.DataContext.SaveChanges();
        }
    }
}
