﻿using Acorna.Core.DTOs;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Notification;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Service.Project.BillingSystem
{
    internal class ComingNumbersService : IComingNumbersService
    {
        private readonly IUnitOfWork _unitOfWork;

        internal ComingNumbersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationRecord<PhoneBookModel>> GetComingNumbers(int pageIndex, int pageSize, int statusNumber, int currentUserId, string currentUserRole)
        {
            RolesType rolesType = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);
            PaginationRecord<PhoneBookModel> PhoneBookModel = new PaginationRecord<PhoneBookModel>();

            try
            {
                if (rolesType == RolesType.SuperAdmin)
                {
                    PhoneBookModel = await _unitOfWork.IncomingNumbersRepository.GetAllIncomingNumbers(pageIndex, pageSize, statusNumber);
                }
                else if (rolesType == RolesType.AdminGroup)
                {
                    PhoneBookModel = await _unitOfWork.IncomingNumbersRepository.GetIncomingNumbersByGroupId(pageIndex, pageSize, statusNumber, currentUserId);
                }

                return PhoneBookModel;
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

        public List<string> ApprovePhoneNumbers(List<int> phoneNumberId, int currentUserId)
        {
            try
            {
                PhoneBook phoneNumber = new PhoneBook();
                List<PhoneBook> approvePhoneNumbers = new List<PhoneBook>();
                List<PhoneBook> userNotifications = new List<PhoneBook>();
                List<PhoneBook> phoneBooksToUpdateBillsDetails = new List<PhoneBook>();
                List<List<BillDetails>> billDetailsToUpdate = new List<List<BillDetails>>();
                List<string> usersIdToSendApproved = new List<string>();

                phoneNumberId.ForEach(id =>
                {
                    phoneNumber = _unitOfWork.GetRepository<PhoneBook>().GetSingle(id);
                    phoneNumber.StatusNumberId = (int)StatusCycleBills.Approved;
                    phoneNumber.StatusAdminId = currentUserId;

                    approvePhoneNumbers.Add(phoneNumber);

                    if (!userNotifications.Exists(x => x.ReferanceNotificationId == phoneNumber.ReferanceNotificationId))
                    {
                        userNotifications.Add(phoneNumber);
                    }

                    if (!usersIdToSendApproved.Contains(Convert.ToString(phoneNumber.CreatedBy)))
                    {
                        usersIdToSendApproved.Add(Convert.ToString(phoneNumber.CreatedBy));
                    }

                    if (!phoneBooksToUpdateBillsDetails.Contains(phoneNumber))
                    {
                        phoneBooksToUpdateBillsDetails.Add(phoneNumber);
                    }

                    //adding this to update phone number type
                    // ReferanceNotificationId refer to billId;
                    List<BillDetails> billDetails = _unitOfWork.GetRepository<BillDetails>().GetWhere( x => x.BillId == phoneNumber.ReferanceNotificationId && x.PhoneNumber ==  phoneNumber.PhoneNumber).ToList();
                    billDetails.ForEach(details =>
                    {
                        details.TypePhoneNumberId = (int)TypesPhoneNumber.Official;
                        details.PhoneBookId = phoneNumber.Id;
                    });
                    billDetailsToUpdate.Add(billDetails);

                });

                if (approvePhoneNumbers.Count > 0)
                {
                    _unitOfWork.GetRepository<PhoneBook>().UpdateRange(approvePhoneNumbers);
                    _unitOfWork.SaveChanges();

                    //adding this to update bills details phone number type 
                    for (int i = 0; i < billDetailsToUpdate.Count; i++)
                    {
                        _unitOfWork.GetRepository<BillDetails>().UpdateRange(billDetailsToUpdate[i]);
                        _unitOfWork.SaveChanges();
                    }

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "PhoneNumbersSubmittedWasApproved",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.CreatedBy,
                                NotificationTypeId = (int)SystemEnum.NotificationType.PhoneNumbersApproved,
                                RecipientRoleId = 0,
                                ReferenceMassageId = (int)info.ReferanceNotificationId
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

        public List<string> InprogressPhoneNumbers(List<int> phoneNumberId, int currentUserId)
        {
            try
            {
                PhoneBook phoneNumber = new PhoneBook();
                List<PhoneBook> inprogressPhoneNumbers = new List<PhoneBook>();
                List<PhoneBook> userNotifications = new List<PhoneBook>();
                List<string> usersIdToSendInprogress = new List<string>();

                phoneNumberId.ForEach(id =>
                {
                    phoneNumber = _unitOfWork.GetRepository<PhoneBook>().GetSingle(id);
                    phoneNumber.StatusNumberId = (int)StatusCycleBills.InprogressToApproved;
                    phoneNumber.StatusAdminId = currentUserId;

                    inprogressPhoneNumbers.Add(phoneNumber);

                    if (!userNotifications.Exists(x => x.ReferanceNotificationId == phoneNumber.ReferanceNotificationId))
                    {
                        userNotifications.Add(phoneNumber);
                    }

                    if (!usersIdToSendInprogress.Contains(Convert.ToString(phoneNumber.CreatedBy)))
                    {
                        usersIdToSendInprogress.Add(Convert.ToString(phoneNumber.CreatedBy));
                    }
                });


                if (inprogressPhoneNumbers.Count > 0)
                {
                    _unitOfWork.GetRepository<PhoneBook>().UpdateRange(inprogressPhoneNumbers);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "PhoneNumbersSubmittedWasInProgress",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.CreatedBy,
                                NotificationTypeId = (int)SystemEnum.NotificationType.PhoneNumbersInProgress,
                                RecipientRoleId = 0,
                                ReferenceMassageId = (int)info.ReferanceNotificationId
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

        public List<string> RejectPhoneNumbers(List<int> phoneNumberId, int currentUserId)
        {
            try
            {
                PhoneBook phoneNumber = new PhoneBook();
                List<PhoneBook> rejectPhoneNumbers = new List<PhoneBook>();
                List<PhoneBook> userNotifications = new List<PhoneBook>();
                List<string> usersIdToSendRejected = new List<string>();

                phoneNumberId.ForEach(id =>
                {
                    phoneNumber = _unitOfWork.GetRepository<PhoneBook>().GetSingle(id);
                    phoneNumber.StatusNumberId = (int)StatusCycleBills.Rejected;
                    phoneNumber.StatusAdminId = currentUserId;

                    rejectPhoneNumbers.Add(phoneNumber);

                    if (!userNotifications.Exists(x => x.ReferanceNotificationId == phoneNumber.ReferanceNotificationId))
                    {
                        userNotifications.Add(phoneNumber);
                    }

                    if (!usersIdToSendRejected.Contains(Convert.ToString(phoneNumber.CreatedBy)))
                    {
                        usersIdToSendRejected.Add(Convert.ToString(phoneNumber.CreatedBy));
                    }
                });


                if (rejectPhoneNumbers.Count > 0)
                {
                    _unitOfWork.GetRepository<PhoneBook>().UpdateRange(rejectPhoneNumbers);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        userNotifications.ForEach(info =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "PhoneNumbersSubmittedWasRejected",
                                IsRead = false,
                                Deleted = false,
                                RecipientId = info.CreatedBy,
                                NotificationTypeId = (int)SystemEnum.NotificationType.PhoneNumbersRejected,
                                RecipientRoleId = 0,
                                ReferenceMassageId = (int)info.ReferanceNotificationId
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
