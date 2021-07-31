﻿using Acorna.Core.DTOs;
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

        public bool DefinitionNewNumbers(List<UnDefinedNumbersDTO> phoneNumbers, string billId, int currentUserId)
        {
            try
            {
                List<PhoneBook> addPhoneBookOfficial = new List<PhoneBook>();
                List<PhoneBook> addPhoneBookPrivate = new List<PhoneBook>();
                List<PhoneBook> updatePhoneBookOfficial = new List<PhoneBook>();
                List<PhoneBook> updatePhoneBookPrivate = new List<PhoneBook>();

                phoneNumbers.ForEach(item =>
                {
                    PhoneBook phoneBook = _unitOfWork.GetRepository<PhoneBook>().GetSingleAsync(item.Id).Result;

                    if (!string.IsNullOrEmpty(item.PhoneName) && item.TypePhoneNumberId != (int)TypesPhoneNumber.Personal && item.TypePhoneNumberId != (int)TypesPhoneNumber.Unknown)
                    {
                        if (phoneBook == null)
                        {
                            //adding a new phone number
                            addPhoneBookOfficial.Add(new PhoneBook
                            {
                                PhoneNumber = item.DialledNumber,
                                PhoneName = item.PhoneName,
                                TypePhoneNumberId = item.TypePhoneNumberId,
                                PersonalUserId = 0,
                                StatusNumberId = (int)StatusCycleBills.Submit,
                                ReferanceNotificationId = Convert.ToInt32(billId)
                            });
                        }
                        else
                        {
                            phoneBook.PhoneName = item.PhoneName;
                            phoneBook.TypePhoneNumberId = item.TypePhoneNumberId;
                            phoneBook.PersonalUserId = 0;

                            //updating exists phone number
                            updatePhoneBookOfficial.Add(phoneBook);
                        }
                    }

                    if (!string.IsNullOrEmpty(item.PhoneName) && item.TypePhoneNumberId == (int)TypesPhoneNumber.Personal)
                    {
                        if (phoneBook == null)
                        {
                            //adding a new phone number
                            addPhoneBookPrivate.Add(new PhoneBook
                            {
                                PhoneNumber = item.DialledNumber,
                                PhoneName = item.PhoneName,
                                TypePhoneNumberId = item.TypePhoneNumberId,
                                PersonalUserId = currentUserId,
                                StatusNumberId = (int)StatusCycleBills.Submit,
                                ReferanceNotificationId = Convert.ToInt32(billId)
                            });
                        }
                        else
                        {
                            phoneBook.PhoneName = item.PhoneName;
                            phoneBook.TypePhoneNumberId = item.TypePhoneNumberId;
                            phoneBook.PersonalUserId = currentUserId;

                            //updating exists phone number
                            updatePhoneBookPrivate.Add(phoneBook);
                        }
                    }
                });

                _unitOfWork.GetRepository<PhoneBook>().InsertRange(addPhoneBookOfficial);
                _unitOfWork.GetRepository<PhoneBook>().UpdateRange(updatePhoneBookOfficial);

                _unitOfWork.GetRepository<PhoneBook>().InsertRange(addPhoneBookPrivate);
                _unitOfWork.GetRepository<PhoneBook>().UpdateRange(updatePhoneBookPrivate);

                _unitOfWork.SaveChanges();

                if (addPhoneBookOfficial.Count > 0 || updatePhoneBookOfficial.Count > 0)
                {
                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                        {
                            MessageText = "NewPhoneNumbersWasSubmitted",
                            IsRead = false,
                            Deleted = false,
                            ReferenceMassageId = Convert.ToInt32(billId),
                            NotificationTypeId = (int)SystemEnum.NotificationType.PhoneNumbersSubmitted,
                            RecipientRoleId = (int)SystemEnum.RolesType.SuperAdmin
                        });
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateSubmitBill(int billId)
        {
            try
            {
                Bill bill = _unitOfWork.GetRepository<Bill>().GetSingle(billId);

                if (bill != null)
                {
                    bill.SubmittedByUser = true;

                    _unitOfWork.GetRepository<Bill>().Update(bill);
                    _unitOfWork.SaveChanges();

                    if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                    {
                        _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                        {
                            MessageText = "NewBillsWasSubmitted",
                            IsRead = false,
                            Deleted = false,
                            ReferenceMassageId = Convert.ToInt32(billId),
                            NotificationTypeId = (int)SystemEnum.NotificationType.BillSubmitted,
                            RecipientRoleId = (int)SystemEnum.RolesType.SuperAdmin
                        });
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}