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
    internal class ComingServicesService : IComingServicesService
    {
        private readonly IUnitOfWork _unitOfWork;

        internal ComingServicesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationRecord<ServicesNeedApprovedDTO>> GetComingServices(int pageIndex, int pageSize, int statusNumber, int currentUserId, string currentUserRole)
        {
            RolesType rolesType = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);
            PaginationRecord<ServicesNeedApprovedDTO> ServicesNeedApprovedDTO = new PaginationRecord<ServicesNeedApprovedDTO>();

            try
            {
                if (rolesType == RolesType.SuperAdmin)
                {
                    ServicesNeedApprovedDTO = await _unitOfWork.ComingServicesRepository.GetAllComingServices(pageIndex, pageSize, statusNumber);
                }
                else if (rolesType == RolesType.AdminGroup)
                {
                    ServicesNeedApprovedDTO = await _unitOfWork.ComingServicesRepository.GetComingServicesByGroupId(pageIndex, pageSize, statusNumber, currentUserId);
                }

                return ServicesNeedApprovedDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountRecord()
        {
            try
            {
                return _unitOfWork.GetRepository<PhoneBook>().GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> ApproveServices(List<int> serviceDetailsId, int currentUserId)
        {
            try
            {
                BillDetails serviceDetails = new BillDetails();
                List<BillDetails> approveServicesDetails = new List<BillDetails>();
                List<Bill> userNotifications = new List<Bill>();
                List<string> usersIdToSendApproved = new List<string>();

                serviceDetailsId.ForEach(id =>
                {
                    serviceDetails = _unitOfWork.GetRepository<BillDetails>().GetSingle(id);

                    if (serviceDetails != null)
                    {
                        serviceDetails.StatusServiceUsedId = (int)StatusCycleBills.Approved;
                        serviceDetails.StatusServiceUsedBy = currentUserId;

                        approveServicesDetails.Add(serviceDetails);

                        //get user-Id from Bill-Id
                        Bill bill = _unitOfWork.GetRepository<Bill>().GetSingle(serviceDetails.BillId);

                        if (!usersIdToSendApproved.Contains(Convert.ToString(bill.UserId)))
                        {
                            usersIdToSendApproved.Add(Convert.ToString(bill.UserId));
                        }

                        if (!userNotifications.Exists(x => x.Id == bill.Id))
                        {
                            userNotifications.Add(bill);
                        }
                    }
                });

                if (approveServicesDetails.Count > 0)
                {
                    _unitOfWork.GetRepository<BillDetails>().UpdateRange(approveServicesDetails);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "ServicesSubmittedWasApproved",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.UserId,
                                NotificationTypeId = (int)SystemEnum.NotificationType.ServicesApproved,
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

        public List<string> InprogressServices(List<int> ServiceDetailsId, int currentUserId)
        {
            try
            {
                BillDetails serviceDetails = new BillDetails();
                List<BillDetails> inprogressServices = new List<BillDetails>();
                List<Bill> userNotifications = new List<Bill>();
                List<string> usersIdToSendInprogress = new List<string>();

                ServiceDetailsId.ForEach(id =>
                {
                    serviceDetails = _unitOfWork.GetRepository<BillDetails>().GetSingle(id);
                    serviceDetails.StatusServiceUsedId = (int)StatusCycleBills.InprogressToApproved;
                    serviceDetails.StatusServiceUsedBy = currentUserId;

                    inprogressServices.Add(serviceDetails);

                    //get user-Id from Bill-Id
                    Bill bill = _unitOfWork.GetRepository<Bill>().GetSingle(serviceDetails.BillId);

                    if (!usersIdToSendInprogress.Contains(Convert.ToString(bill.UserId)))
                    {
                        usersIdToSendInprogress.Add(Convert.ToString(bill.UserId));
                    }

                    if (!userNotifications.Exists(x => x.Id == bill.Id))
                    {
                        userNotifications.Add(bill);
                    }
                });


                if (inprogressServices.Count > 0)
                {
                    _unitOfWork.GetRepository<BillDetails>().UpdateRange(inprogressServices);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "ServicesSubmittedWasInProgress",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.UserId,
                                NotificationTypeId = (int)SystemEnum.NotificationType.PhoneNumbersInProgress,
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

        public List<string> RejectServices(List<int> phoneNumberId, int currentUserId)
        {
            try
            {
                BillDetails serviceDetails = new BillDetails();
                List<BillDetails> rejectServiceDetails = new List<BillDetails>();
                List<Bill> userNotifications = new List<Bill>();
                List<string> usersIdToSendRejected = new List<string>();

                phoneNumberId.ForEach(id =>
                {
                    serviceDetails = _unitOfWork.GetRepository<BillDetails>().GetSingle(id);
                    serviceDetails.StatusServiceUsedId = (int)StatusCycleBills.Rejected;
                    serviceDetails.StatusServiceUsedBy = currentUserId;

                    rejectServiceDetails.Add(serviceDetails);

                    //get user-Id from Bill-Id
                    Bill bill = _unitOfWork.GetRepository<Bill>().GetSingle(serviceDetails.BillId);

                    if (!usersIdToSendRejected.Contains(Convert.ToString(bill.UserId)))
                    {
                        usersIdToSendRejected.Add(Convert.ToString(bill.UserId));
                    }

                    if (!userNotifications.Exists(x => x.Id == bill.Id))
                    {
                        userNotifications.Add(bill);
                    }
                });


                if (rejectServiceDetails.Count > 0)
                {
                    _unitOfWork.GetRepository<BillDetails>().UpdateRange(rejectServiceDetails);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "ServicesSubmittedWasRejected",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.UserId,
                                NotificationTypeId = (int)SystemEnum.NotificationType.ServicesRejected,
                                RecipientRoleId = 0,
                                ReferenceMassageId = info.Id
                            });
                        });
                    }

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderByEmail())
                    {
                        usersIdToSendRejected.ForEach(userId =>
                        {
                            _unitOfWork.EmailRepository.RejectNumberEmail(_unitOfWork.SecurityRepository.GetEmailByUserId(Convert.ToInt32(userId)).Result);
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
    }
}
