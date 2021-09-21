using Acorna.Core.DTOs;
using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Notification;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Service.Project.BillingSystem
{
    internal class ComingBillsService : IComingBillsService
    {
        private readonly IUnitOfWork _unitOfWork;

        internal ComingBillsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationRecord<BillsSummaryDTO>> GetComingBills(int pageIndex, int pageSize, int statusNumber, int currentUserId, string currentUserRole)
        {
            RolesType rolesType = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);
            PaginationRecord<BillsSummaryDTO> comingBillsDTO = new PaginationRecord<BillsSummaryDTO>();

            try
            {
                if (rolesType == RolesType.SuperAdmin)
                {
                    comingBillsDTO = await _unitOfWork.ComingBillsRepository.GetAllComingBills(pageIndex, pageSize, statusNumber);
                }
                else if (rolesType == RolesType.AdminGroup)
                {
                    comingBillsDTO = await _unitOfWork.ComingBillsRepository.GetComingBillsByGroupId(pageIndex, pageSize, statusNumber, currentUserId);
                }
                else if (rolesType == RolesType.Finance)
                {
                    //
                    //comingBillsDTO = await _unitOfWork.ComingBillsRepository.GetAllComingBills(pageIndex, pageSize, statusNumber);
                }

                return comingBillsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> ApproveBills(List<int> billsIds)
        {
            try
            {
                Bill bill = new Bill();
                List<Bill> approveBills = new List<Bill>();
                List<Bill> userNotifications = new List<Bill>();
                List<string> usersIdToSendApproved = new List<string>();

                billsIds.ForEach(id =>
                {
                    bill = _unitOfWork.GetRepository<Bill>().GetSingle(id);
                    bill.StatusBillId = (int)StatusCycleBills.Approved;
                    bill.SubmittedByAdmin = true;

                    approveBills.Add(bill);

                    if (!userNotifications.Exists(x => x.Id == bill.Id))
                    {
                        userNotifications.Add(bill);
                    }

                    if (!usersIdToSendApproved.Contains(Convert.ToString(bill.UserId)))
                    {
                        usersIdToSendApproved.Add(Convert.ToString(bill.UserId));
                    }
                });

                if (approveBills.Count > 0)
                {
                    _unitOfWork.GetRepository<Bill>().UpdateRange(approveBills);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "BillsSubmittedWasApproved",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.UserId,
                                NotificationTypeId = (int)SystemEnum.NotificationType.BillApproved,
                                RecipientRoleId = 0,
                                ReferenceMassageId = info.Id
                            });
                        });
                    }
                }
                return usersIdToSendApproved;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> InprogressBills(List<int> billsIds)
        {
            try
            {
                Bill bill = new Bill();
                List<Bill> inprogressBills = new List<Bill>();
                List<Bill> userNotifications = new List<Bill>();
                List<string> usersIdToSendInprogress = new List<string>();

                billsIds.ForEach(id =>
                {
                    bill = _unitOfWork.GetRepository<Bill>().GetSingle(id);
                    bill.StatusBillId = (int)StatusCycleBills.InprogressToApproved;
                    bill.SubmittedByAdmin = false;

                    inprogressBills.Add(bill);

                    if (!userNotifications.Exists(x => x.Id == bill.Id))
                    {
                        userNotifications.Add(bill);
                    }

                    if (!usersIdToSendInprogress.Contains(Convert.ToString(bill.Id)))
                    {
                        usersIdToSendInprogress.Add(Convert.ToString(bill.Id));
                    }
                });


                if (inprogressBills.Count > 0)
                {
                    _unitOfWork.GetRepository<Bill>().UpdateRange(inprogressBills);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "BillsSubmittedWasInProgress",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.UserId,
                                NotificationTypeId = (int)SystemEnum.NotificationType.BillInProgress,
                                RecipientRoleId = 0,
                                ReferenceMassageId = info.Id
                            });
                        });
                    }
                }

                return usersIdToSendInprogress;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> RejectBills(List<int> billsIds)
        {
            try
            {
                Bill bill = new Bill();
                List<Bill> rejectBills = new List<Bill>();
                List<Bill> userNotifications = new List<Bill>();
                List<string> usersIdToSendRejected = new List<string>();

                billsIds.ForEach(id =>
                {
                    bill = _unitOfWork.GetRepository<Bill>().GetSingle(id);
                    bill.StatusBillId = (int)StatusCycleBills.Rejected;
                    bill.SubmittedByAdmin = false;

                    rejectBills.Add(bill);

                    if (!userNotifications.Exists(x => x.Id == bill.Id))
                    {
                        userNotifications.Add(bill);
                    }

                    if (!usersIdToSendRejected.Contains(Convert.ToString(bill.Id)))
                    {
                        usersIdToSendRejected.Add(Convert.ToString(bill.Id));
                    }
                });


                if (rejectBills.Count > 0)
                {
                    _unitOfWork.GetRepository<Bill>().UpdateRange(rejectBills);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "BillsSubmittedWasRejected",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.UserId,
                                NotificationTypeId = (int)SystemEnum.NotificationType.BillRejected,
                                RecipientRoleId = 0,
                                ReferenceMassageId = info.Id
                            });
                        });
                    }
                }

                return usersIdToSendRejected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> PayBills(List<int> billsIds)
        {
            try
            {
                Bill bill = new Bill();
                List<Bill> PayBills = new List<Bill>();
                List<Bill> userNotifications = new List<Bill>();
                List<string> usersIdToSendPaid = new List<string>();

                billsIds.ForEach(id =>
                {
                    bill = _unitOfWork.GetRepository<Bill>().GetSingle(id);

                    if (bill.SubmittedByAdmin && !bill.IsPaid)
                    {
                        bill.IsPaid = true;

                        PayBills.Add(bill);

                        if (!userNotifications.Exists(x => x.Id == bill.Id))
                        {
                            userNotifications.Add(bill);
                        }

                        if (!usersIdToSendPaid.Contains(Convert.ToString(bill.Id)))
                        {
                            usersIdToSendPaid.Add(Convert.ToString(bill.Id));
                        }
                    }

                });


                if (PayBills.Count > 0)
                {
                    _unitOfWork.GetRepository<Bill>().UpdateRange(PayBills);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "BillsSubmittedWasPaid",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.UserId,
                                NotificationTypeId = (int)SystemEnum.NotificationType.BillPaid,
                                RecipientRoleId = 0,
                                ReferenceMassageId = info.Id
                            });
                        });
                    }

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderByEmail())
                    {
                        usersIdToSendPaid.ForEach(userId =>
                        {
                            _unitOfWork.EmailRepository.PaidEmail(_unitOfWork.SecurityRepository.GetEmailByUserId(userId).Result);
                        });
                    }
                }

                return usersIdToSendPaid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
