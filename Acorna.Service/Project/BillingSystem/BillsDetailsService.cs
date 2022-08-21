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
    public class BillsDetailsService : IBillsDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BillsDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationRecord<UnDefinedNumbersDTO>> GetAllUndefinedNumbers(int billId)
        {
            try
            {
                return await _unitOfWork.BillsDetailsRepository.GetAllUndefinedNumbers(billId);
            }
            catch (Exception ex)
                {
                throw ex;
            }
        }

        public List<string> DefinitionNewNumbers(List<UnDefinedNumbersDTO> phoneNumbers, string billId, int currentUserId)
        {
            try
            {
                List<PhoneBook> addPhoneBookOfficial = new List<PhoneBook>();
                List<PhoneBook> addPhoneBookPrivate = new List<PhoneBook>();
                List<PhoneBook> updatePhoneBookOfficial = new List<PhoneBook>();
                List<PhoneBook> updatePhoneBookPrivate = new List<PhoneBook>();
                List<string> usersId = new List<string>();
                bool isAutomatedApprovalOfNumbers = _unitOfWork.GeneralSettingsRepository.IsAutomatedApprovalOfNumbers();

                phoneNumbers.ForEach(item =>
                {
                    PhoneBook phoneBook = _unitOfWork.GetRepository<PhoneBook>().GetSingleAsync(item.Id).Result;

                    if (!string.IsNullOrEmpty(item.PhoneName) && item.TypePhoneNumberId == (int)TypesPhoneNumber.Official)
                    {
                        if (phoneBook == null)
                        {
                            //add a new phone number
                            addPhoneBookOfficial.Add(new PhoneBook
                            {
                                PhoneNumber = item.DialledNumber,
                                PhoneName = item.PhoneName,
                                TypePhoneNumberId = item.TypePhoneNumberId,
                                PersonalUserId = 0,
                                StatusNumberId = (isAutomatedApprovalOfNumbers) ? (int)StatusCycleBills.Approved
                                 : (int)StatusCycleBills.Submit,
                                ReferanceNotificationId = Convert.ToInt32(billId)
                            });
                        }
                        else
                        {
                            phoneBook.PhoneName = item.PhoneName;
                            phoneBook.TypePhoneNumberId = item.TypePhoneNumberId;
                            phoneBook.PersonalUserId = currentUserId;

                            //update exists phone number
                            updatePhoneBookOfficial.Add(phoneBook);
                        }
                    }

                    if (!string.IsNullOrEmpty(item.PhoneName) && item.TypePhoneNumberId == (int)TypesPhoneNumber.Personal)
                    {
                        if (phoneBook == null)
                        {
                            //add a new phone number
                            addPhoneBookPrivate.Add(new PhoneBook
                            {
                                PhoneNumber = item.DialledNumber,
                                PhoneName = item.PhoneName,
                                TypePhoneNumberId = item.TypePhoneNumberId,
                                PersonalUserId = currentUserId,
                                StatusNumberId = (isAutomatedApprovalOfNumbers) ? (int)StatusCycleBills.Approved
                                 : (int)StatusCycleBills.Submit,
                                ReferanceNotificationId = Convert.ToInt32(billId)
                            });
                        }
                        else
                        {
                            phoneBook.PhoneName = item.PhoneName;
                            phoneBook.TypePhoneNumberId = item.TypePhoneNumberId;
                            phoneBook.PersonalUserId = currentUserId;

                            //update exists phone number
                            updatePhoneBookPrivate.Add(phoneBook);
                        }
                    }
                });

                _unitOfWork.GetRepository<PhoneBook>().InsertRange(addPhoneBookOfficial);
                _unitOfWork.GetRepository<PhoneBook>().UpdateRange(updatePhoneBookOfficial);

                _unitOfWork.GetRepository<PhoneBook>().InsertRange(addPhoneBookPrivate);
                _unitOfWork.GetRepository<PhoneBook>().UpdateRange(updatePhoneBookPrivate);

                _unitOfWork.SaveChanges();

                if (!isAutomatedApprovalOfNumbers)
                {
                    if (addPhoneBookOfficial.Count > 0 || updatePhoneBookOfficial.Count > 0)
                    {
                        if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                        {
                            usersId = _unitOfWork.SecurityRepository.GetSuperAdminWithAdminGropByUserId(currentUserId).Result;

                            usersId.ForEach(userId =>
                            {
                                _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                                {
                                    MessageText = "NewPhoneNumbersWasSubmitted",
                                    IsRead = false,
                                    Deleted = false,
                                    ReferenceMassageId = Convert.ToInt32(billId),
                                    NotificationTypeId = (int)SystemEnum.NotificationType.PhoneNumbersSubmitted,
                                    RecipientId = Convert.ToInt32(userId)
                                });
                            });

                        }

                        if (_unitOfWork.GeneralSettingsRepository.IsReminderByEmail())
                        {
                            usersId.ForEach(userId =>
                            {
                                _unitOfWork.EmailRepository.ReminderIdentifyNewNumbersEmail(_unitOfWork.SecurityRepository.GetEmailByUserId(Convert.ToInt32(userId)).Result);

                            });
                        }
                    }
                }

                return usersId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> ServicesSubmitted(List<ServicesNeedApprovedDTO> servicesNeedApproval, string billId, int currentUserId)
        {
            try
            {   
                List<BillDetails> updateCallDetails = new List<BillDetails>();
                List<string> usersId = new List<string>();
                bool isAutomatedApprovalServices = _unitOfWork.GeneralSettingsRepository.IsAutomatedApprovalServices();

                servicesNeedApproval.ForEach(callDetails =>
                {
                    BillDetails billDetails = _unitOfWork.GetRepository<BillDetails>().GetSingleAsync(callDetails.Id).Result;

                    if (billDetails != null && callDetails.TypeServiceUsedId != (int)TypesPhoneNumber.Unknown)
                    {
                        billDetails.TypeServiceUsedId = callDetails.TypeServiceUsedId;
                        billDetails.StatusServiceUsedId = (isAutomatedApprovalServices) ? (int)StatusCycleBills.Approved : (int)StatusCycleBills.Submit;
                        updateCallDetails.Add(billDetails);
                    }
                });

                _unitOfWork.GetRepository<BillDetails>().UpdateRange(updateCallDetails);
                _unitOfWork.SaveChanges();

                if (!isAutomatedApprovalServices)
                {
                    if (updateCallDetails.Count > 0)
                    {
                        if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                        {
                            usersId = _unitOfWork.SecurityRepository.GetSuperAdminWithAdminGropByUserId(currentUserId).Result;

                            usersId.ForEach(userId =>
                            {
                                _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                                {
                                    MessageText = "NewServicesWasSubmitted",
                                    IsRead = false,
                                    Deleted = false,
                                    ReferenceMassageId = Convert.ToInt32(billId),
                                    NotificationTypeId = (int)SystemEnum.NotificationType.ServicesSubmitted,
                                    RecipientId = Convert.ToInt32(userId)
                                });
                            });

                        }

                        if (_unitOfWork.GeneralSettingsRepository.IsReminderByEmail())
                        {
                            usersId.ForEach(userId =>
                            {
                                _unitOfWork.EmailRepository.ReminderIdentifyNewNumbersEmail(_unitOfWork.SecurityRepository.GetEmailByUserId(Convert.ToInt32(userId)).Result);

                            });
                        }
                    }
                }

                return usersId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> UpdateSubmitBill(int billId)
        {
            try
            {
                Bill bill = _unitOfWork.GetRepository<Bill>().GetSingle(billId);
                List<string> usersId = new List<string>();

                if (bill != null)
                {
                    if (_unitOfWork.GeneralSettingsRepository.IsAutomatedApprovalBills())
                    {
                        bill.SubmittedByUser = true;
                        bill.SubmittedByAdmin = true;
                        bill.StatusBillId = (int)SystemEnum.StatusCycleBills.Approved;
                        bill.SubmittedDate = DateTime.Now;
                    }
                    else
                    {
                        bill.SubmittedByUser = true;
                        bill.StatusBillId = (int)SystemEnum.StatusCycleBills.Submit;
                        bill.SubmittedDate = DateTime.Now;
                    }


                    _unitOfWork.GetRepository<Bill>().Update(bill);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem() && !_unitOfWork.GeneralSettingsRepository.IsAutomatedApprovalBills())
                    {
                        usersId = _unitOfWork.SecurityRepository.GetSuperAdminWithAdminGropByUserId(bill.UserId).Result;

                        usersId.ForEach(userId =>
                        {
                            _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                            {
                                MessageText = "NewBillsWasSubmitted",
                                IsRead = false,
                                Deleted = false,
                                ReferenceMassageId = Convert.ToInt32(billId),
                                NotificationTypeId = (int)SystemEnum.NotificationType.BillSubmitted,
                                RecipientId = Convert.ToInt32(userId)
                            });
                        });
                    }
                }

                if (_unitOfWork.GeneralSettingsRepository.IsReminderByEmail() && !_unitOfWork.GeneralSettingsRepository.IsAutomatedApprovalBills())
                {
                    usersId.ForEach(userId =>
                    {
                        _unitOfWork.EmailRepository.SubmittedBillEmail(_unitOfWork.SecurityRepository.GetEmailByUserId(Convert.ToInt32(userId)).Result);

                    });
                }

                return usersId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginationRecord<ServicesNeedApprovedDTO>> GetServicesNeedApproval(int billId)
        {
            try
            {
                return await _unitOfWork.BillsDetailsRepository.GetServicesNeedApproval(billId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
