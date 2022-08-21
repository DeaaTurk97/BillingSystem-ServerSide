using Acorna.CommonMember;
using Acorna.Core.DTOs;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Notification;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using AutoMapper;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;


namespace Acorna.Service.Project.BillingSystem
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        internal BillService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Syria ...

        public List<string> UploadMTNBills(List<DocumentModel> filesUploaded, int currentUserId)
        {
            try
            {
                IFormatProvider enUsDateFormat = new CultureInfo("en-US").DateTimeFormat;
                List<Bill> bills = new List<Bill>();
                List<BillDetails> billsDetails = new List<BillDetails>();
                List<string> usersIDUplodedBills = new List<string>();
                int phoneBookId = 0;
                string dialedNumber = string.Empty;
                int TypePhoneNumberId = 0;
                int serviceTypeId = 0;
                bool isServiceNeedApproved = false;
                int operatrId = 0;
                string operatorKey = string.Empty;
                string dataUsage = string.Empty;
                string userPhoneNumber = string.Empty;
                string callDuration = string.Empty;
                int userId = 0;


                foreach (DocumentModel file in filesUploaded)
                {
                    using (var stream = System.IO.File.Open(file.URL, FileMode.Open, FileAccess.Read))
                    {
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet datasetBills = reader.AsDataSet();
                            DataTable dataTable = datasetBills.Tables[0];

                            dataTable = dataTable.AsEnumerable()
                                                 .GroupBy(r => r.Field<object>("Column9"))
                                                 .SelectMany(x => x)
                                                 .CopyToDataTable();

                            while (reader.Read()) //Each row of the file
                            {
                                if (reader.GetValue(0).ToString().Trim().Contains("customer_id"))
                                {
                                    continue;
                                }

                                //check if this user is Exist or not
                                userPhoneNumber = SyriaUtilites.GetNumberWithoutCountryKey(Convert.ToString(reader.GetValue(1)).Trim());
                                if (userId == 0)
                                {
                                    if (_unitOfWork.SecurityRepository.IsUserExistsByPhoneNumber(userPhoneNumber).Result)
                                    {
                                        userId = _unitOfWork.SecurityRepository.SearchByPhoneNumber(userPhoneNumber).Result;
                                    }
                                    else
                                    {
                                        userId = _unitOfWork.SecurityRepository.CreateUserUsingPhoneNumber(userPhoneNumber, (int)SimType.Voice, (int)SimProfileType.Activated).Result;
                                    }
                                }

                                //check if current row for called_number column is number
                                if (SyriaUtilites.IsStringNumber(Convert.ToString(reader.GetValue(2)).Trim()))
                                {
                                    dialedNumber = SyriaUtilites.GetNumberWithoutCountryKey(Convert.ToString(reader.GetValue(2)).Trim());
                                    PhoneBook phoneBook = _unitOfWork.GetRepository<PhoneBook>().FirstOrDefault(x => x.PhoneNumber == dialedNumber);

                                    if (phoneBook != null)
                                    {
                                        phoneBookId = phoneBook.Id;
                                        TypePhoneNumberId = phoneBook.TypePhoneNumberId;

                                        operatorKey = SyriaUtilites.GetOperatorKeyFromNumber(Convert.ToString(reader.GetValue(2)).Trim());
                                        Operator operatr = _unitOfWork.GetRepository<Operator>().FirstOrDefault(x => x.OperatorKey == Convert.ToInt64(operatorKey));
                                        operatrId = operatr != null ? operatr.Id : 0;
                                    }
                                    else
                                    {
                                        phoneBookId = 0;
                                        TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                        operatrId = 0;
                                    }
                                }
                                else
                                {
                                    dialedNumber = Convert.ToString(reader.GetValue(2)).Trim();
                                    phoneBookId = 0;
                                    TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                    operatrId = 0;
                                }


                                if (!string.IsNullOrEmpty(Convert.ToString(reader.GetValue(6)).Trim()))
                                {
                                    ServiceUsed serviceUsed = _unitOfWork.GetRepository<ServiceUsed>().FirstOrDefault(x => x.ServiceUsedNameAr == Convert.ToString(reader.GetValue(6)) || x.ServiceUsedNameEn == Convert.ToString(reader.GetValue(6)));

                                    if (serviceUsed != null)
                                    {
                                        //Adding this to check if (call_source) column is a service, if not get value of (serviceType)
                                        ServiceUsed sourceServiceUsed = _unitOfWork.GetRepository<ServiceUsed>().FirstOrDefault(x => x.ServiceUsedNameAr == Convert.ToString(reader.GetValue(5)) || x.ServiceUsedNameEn == Convert.ToString(reader.GetValue(5)));

                                        if (sourceServiceUsed != null)
                                        {
                                            serviceTypeId = sourceServiceUsed.Id;
                                            isServiceNeedApproved = sourceServiceUsed.IsNeedApproved;
                                        }
                                        else
                                        {
                                            serviceTypeId = serviceUsed.Id;
                                            isServiceNeedApproved = serviceUsed.IsNeedApproved;
                                        }
                                    }
                                    else
                                    {
                                        //adding a new service type if type Not Exist
                                        ServiceUsed addingServiceUsed = new ServiceUsed
                                        {
                                            ServiceUsedNameAr = Convert.ToString(reader.GetValue(6)).Trim(),
                                            ServiceUsedNameEn = Convert.ToString(reader.GetValue(6)).Trim(),
                                            IsCalculatedValue = true,
                                            IsNeedApproved = false
                                        };

                                        _unitOfWork.GetRepository<ServiceUsed>().Insert(addingServiceUsed);

                                        if (!_unitOfWork.SaveChanges())
                                        {
                                            throw new Exception("Error occurred when inserting a new service");
                                        }

                                        serviceTypeId = addingServiceUsed.Id;
                                        isServiceNeedApproved = addingServiceUsed.IsNeedApproved;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToString(reader.GetValue(2)).Trim().Contains("bundle"))
                                    {
                                        ServiceUsed serviceBundle = _unitOfWork.GetRepository<ServiceUsed>().FirstOrDefault(x => x.ServiceUsedNameAr == "EBU_حزمة" || x.ServiceUsedNameEn == "Bundel");
                                        serviceTypeId = serviceBundle.Id;
                                    }
                                    else
                                    {
                                        //service Number one is empty or ????
                                        serviceTypeId = 1;
                                    }
                                }

                                //when this service is INTERNET
                                if (Convert.ToString(reader.GetValue(7)).Trim().Contains("KB") && !string.IsNullOrEmpty(Convert.ToString(reader.GetValue(7)).Trim()))
                                {
                                    dataUsage = Convert.ToString(reader.GetValue(7)).Trim();
                                    callDuration = "-";
                                }
                                else
                                {
                                    callDuration = Convert.ToString(reader.GetValue(7)).Trim();
                                    dataUsage = "-";
                                }

                                billsDetails.Add(new BillDetails
                                {
                                    PhoneBookId = phoneBookId,
                                    CallDateTime = Convert.ToDateTime(reader.GetValue(3), enUsDateFormat),
                                    CallDuration = callDuration,
                                    CallNetPrice = Convert.ToDecimal(reader.GetValue(9)),
                                    CallRetailPrice = Convert.ToDecimal(reader.GetValue(9)),
                                    PhoneNumber = dialedNumber,
                                    TypePhoneNumberId = TypePhoneNumberId,
                                    ServiceUsedId = serviceTypeId,
                                    IsServiceUsedNeedApproved = isServiceNeedApproved,
                                    TypeServiceUsedId = (int)TypesPhoneNumber.Unknown,
                                    StatusServiceUsedId = (int)SystemEnum.StatusCycleBills.Upload,
                                    OperatorId = operatrId,
                                    DataUsage = dataUsage,
                                });

                            }

                            if (billsDetails.Count > 0)
                            {
                                //chicking if current bill is added
                                if (!IsBillExist(SyriaUtilites.GetDateLastDayMonth(Convert.ToDateTime(reader.GetValue(3), enUsDateFormat)), userId))
                                {
                                    bills.Add(new Bill
                                    {
                                        UserId = userId,
                                        BillDate = SyriaUtilites.GetDateLastDayMonth(Convert.ToDateTime(reader.GetValue(3), enUsDateFormat)),
                                        SubmittedByAdmin = false,
                                        SubmittedByUser = false,
                                        IsPaid = false,
                                        IsTerminal = false,
                                        StatusBillId = (int)SystemEnum.StatusCycleBills.Upload,
                                        BillDetails = billsDetails,
                                    });
                                }
                            }
                        }
                        // to reset userId after inserted bill
                        billsDetails = new List<BillDetails>();
                        userId = 0;
                    }
                    //}
                }

                _unitOfWork.GetRepository<Bill>().InsertRange(bills);
                _unitOfWork.SaveChanges();

                if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                {
                    bills.ForEach(bill =>
                    {
                        _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                        {
                            MessageText = "NewBillsWasUploaded",
                            IsRead = false,
                            Deleted = false,
                            RecipientId = bill.UserId,
                            ReferenceMassageId = bill.Id,
                            NotificationTypeId = (int)SystemEnum.NotificationType.BillUploaded,
                            RecipientRoleId = 0
                        });

                        usersIDUplodedBills.Add(Convert.ToString(bill.UserId));
                    });
                }

                if (_unitOfWork.GeneralSettingsRepository.IsReminderByEmail())
                {
                    usersIDUplodedBills.ForEach(userId =>
                    {
                        _unitOfWork.EmailRepository.ImportBillEmail(_unitOfWork.SecurityRepository.GetEmailByUserId(Convert.ToInt32(userId)).Result);
                    });
                }

                return usersIDUplodedBills;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> UploadSyriaTelBills(List<DocumentModel> filesUploaded, int currentUserId)
        {
            IFormatProvider enUsDateFormat = new CultureInfo("en-US").DateTimeFormat;
            List<Bill> bills = new List<Bill>();
            List<BillDetails> billsDetails = new List<BillDetails>();
            List<string> usersIDUplodedBills = new List<string>();
            int phoneBookId = 0;
            string dialedNumber = string.Empty;
            int TypePhoneNumberId = 0;
            int serviceTypeId = 0;
            bool isServiceNeedApproved = false;
            int operatrId = 0;
            string operatorKey = string.Empty;
            string dataUsage = string.Empty;
            string callDuration = string.Empty;
            string userPhoneNumber = string.Empty;
            int userId = 0;

            try
            {
                foreach (DocumentModel file in filesUploaded)
                {
                    using (var stream = System.IO.File.Open(file.URL, FileMode.Open, FileAccess.Read))
                    {
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {

                            DataSet datasetBills = reader.AsDataSet();
                            DataTable dataTable = datasetBills.Tables[0];


                            dataTable = dataTable.AsEnumerable()
                                                 .GroupBy(r => r.Field<object>("Column6"))
                                                 .SelectMany(x => x)
                                                 .CopyToDataTable();



                            for (int index = 0; index < dataTable.Rows.Count; index++)
                            {

                                if (Convert.ToString(dataTable.Rows[index]["Column1"]).Trim().Contains("التاريخ و الساعة"))
                                {
                                    continue;
                                }

                                //check if this user is Exist or not
                                userPhoneNumber = SyriaUtilites.GetNumberWithoutCountryKey(Convert.ToString(dataTable.Rows[index]["Column6"]).Trim());
                                if (userId == 0)
                                {
                                    if (_unitOfWork.SecurityRepository.IsUserExistsByPhoneNumber(userPhoneNumber).Result)
                                    {
                                        userId = _unitOfWork.SecurityRepository.SearchByPhoneNumber(userPhoneNumber).Result;
                                    }
                                    else
                                    {
                                        userId = _unitOfWork.SecurityRepository.CreateUserUsingPhoneNumber(userPhoneNumber, (int)SimType.Voice, (int)SimProfileType.Activated).Result;
                                    }
                                }

                                //check if current row for called_number column is number
                                if (SyriaUtilites.IsStringNumber(Convert.ToString(dataTable.Rows[index]["Column2"]).Trim()))
                                {
                                    dialedNumber = SyriaUtilites.GetNumberWithoutCountryKey(Convert.ToString(dataTable.Rows[index]["Column2"]).Trim());
                                    PhoneBook phoneBook = _unitOfWork.GetRepository<PhoneBook>().FirstOrDefault(x => x.PhoneNumber == dialedNumber);

                                    if (phoneBook != null)
                                    {
                                        phoneBookId = phoneBook.Id;
                                        TypePhoneNumberId = phoneBook.TypePhoneNumberId;

                                        operatorKey = SyriaUtilites.GetOperatorKeyFromNumber(Convert.ToString(dataTable.Rows[index]["Column2"]).Trim());
                                        Operator operatr = _unitOfWork.GetRepository<Operator>().FirstOrDefault(x => x.OperatorKey == Convert.ToInt32(operatorKey));
                                        operatrId = operatr != null ? operatr.Id : 0;
                                    }
                                    else
                                    {
                                        phoneBookId = 0;
                                        TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                        operatrId = 0;
                                    }
                                }
                                else
                                {
                                    dialedNumber = Convert.ToString(dataTable.Rows[index]["Column2"]).Trim();
                                    phoneBookId = 0;
                                    TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                    operatrId = 0;
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(dataTable.Rows[index]["Column3"]).Trim()) && Convert.ToString(dataTable.Rows[index]["Column3"]).Trim() != "????")
                                {
                                    ServiceUsed serviceUsed = _unitOfWork.GetRepository<ServiceUsed>().FirstOrDefault(x => x.ServiceUsedNameAr == Convert.ToString(dataTable.Rows[index]["Column3"]).Trim() || x.ServiceUsedNameEn == Convert.ToString(dataTable.Rows[index]["Column3"]).Trim());
                                    if (serviceUsed != null)
                                    {
                                        serviceTypeId = serviceUsed.Id;
                                        isServiceNeedApproved = serviceUsed.IsNeedApproved;
                                    }
                                    else
                                    {
                                        //adding a new service type if type Not Exist
                                        ServiceUsed addingServiceUsed = new ServiceUsed
                                        {
                                            ServiceUsedNameAr = Convert.ToString(dataTable.Rows[index]["Column3"]).Trim(),
                                            ServiceUsedNameEn = Convert.ToString(dataTable.Rows[index]["Column3"]).Trim(),
                                            IsCalculatedValue = true,
                                            IsNeedApproved = false
                                        };

                                        _unitOfWork.GetRepository<ServiceUsed>().Insert(addingServiceUsed);

                                        if (!_unitOfWork.SaveChanges())
                                        {
                                            throw new Exception("Error occurred when inserting a new service");
                                        }

                                        serviceTypeId = addingServiceUsed.Id;
                                        isServiceNeedApproved = addingServiceUsed.IsNeedApproved;
                                    }
                                }
                                else
                                {
                                    //service Number one is empty or ????
                                    serviceTypeId = 1;
                                }


                                //when this service is INTERNET
                                if (Convert.ToString(dataTable.Rows[index]["Column3"]).Trim().Contains("خدمة"))
                                {
                                    dataUsage = Convert.ToString(dataTable.Rows[index]["Column5"]).Trim();
                                    callDuration = "-";
                                }
                                else
                                {
                                    dataUsage = "-";
                                    callDuration = Convert.ToString(dataTable.Rows[index]["Column5"]).Trim();
                                }

                                billsDetails.Add(new BillDetails
                                {
                                    PhoneBookId = phoneBookId,
                                    CallDuration = callDuration,
                                    CallDateTime = Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat),
                                    CallNetPrice = Convert.ToDecimal(dataTable.Rows[index]["Column4"]),
                                    CallRetailPrice = Convert.ToDecimal(dataTable.Rows[index]["Column4"]),
                                    PhoneNumber = dialedNumber,
                                    TypePhoneNumberId = TypePhoneNumberId,
                                    ServiceUsedId = serviceTypeId,
                                    IsServiceUsedNeedApproved = isServiceNeedApproved,
                                    TypeServiceUsedId = (int)TypesPhoneNumber.Unknown,
                                    StatusServiceUsedId = (int)SystemEnum.StatusCycleBills.Upload,
                                    OperatorId = operatrId,
                                    DataUsage = dataUsage,
                                });

                                //To avoid error index out of range... when read last row
                                if (index + 1 < dataTable.Rows.Count)
                                {
                                    // Adding this condition to check if next row is a new number,
                                    if (SyriaUtilites.GetNumberWithoutCountryKey(dataTable.Rows[index]["Column6"].ToString().Trim()) != SyriaUtilites.GetNumberWithoutCountryKey(dataTable.Rows[index + 1]["Column6"].ToString().Trim()))
                                    {
                                        //chicking if current bill is Exist
                                        if (!IsBillExist(SyriaUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)), userId))
                                        {
                                            bills.Add(new Bill
                                            {
                                                UserId = userId,
                                                BillDate = SyriaUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)),
                                                SubmittedByAdmin = false,
                                                SubmittedByUser = false,
                                                IsPaid = false,
                                                IsTerminal = false,
                                                StatusBillId = (int)SystemEnum.StatusCycleBills.Upload,
                                                BillDetails = billsDetails
                                            });
                                        }
                                        //this means the next row contains a new  Number, Must reset bills details list
                                        billsDetails = new List<BillDetails>();
                                        userId = 0;
                                    }
                                }
                                else
                                {
                                    //chicking if current bill is added
                                    if (!IsBillExist(SyriaUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)), userId))
                                    {
                                        bills.Add(new Bill
                                        {
                                            UserId = userId,
                                            BillDate = SyriaUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)),
                                            SubmittedByAdmin = false,
                                            SubmittedByUser = false,
                                            IsPaid = false,
                                            IsTerminal = false,
                                            StatusBillId = (int)SystemEnum.StatusCycleBills.Upload,
                                            BillDetails = billsDetails
                                        });
                                    }
                                    //this means the next row contains a new  Number, Must reset bills details list
                                    billsDetails = new List<BillDetails>();
                                    userId = 0;
                                }
                            }
                        }
                    }
                }

                _unitOfWork.GetRepository<Bill>().InsertRange(bills);
                _unitOfWork.SaveChanges();

                if (_unitOfWork.GeneralSettingsRepository.IsReminderBySystem())
                {
                    bills.ForEach(bill =>
                    {
                        _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                        {
                            MessageText = "NewBillsWasUploaded",
                            IsRead = false,
                            Deleted = false,
                            RecipientId = bill.UserId,
                            ReferenceMassageId = bill.Id,
                            NotificationTypeId = (int)SystemEnum.NotificationType.BillUploaded,
                            RecipientRoleId = 0
                        });

                        usersIDUplodedBills.Add(Convert.ToString(bill.UserId));
                    });
                }

                if (_unitOfWork.GeneralSettingsRepository.IsReminderByEmail())
                {
                    usersIDUplodedBills.ForEach(userId =>
                    {
                        _unitOfWork.EmailRepository.ImportBillEmail(_unitOfWork.SecurityRepository.GetEmailByUserId(Convert.ToInt32(userId)).Result);
                    });
                }

                return usersIDUplodedBills;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Lebanon ...

        public bool UploadCallsAndRoamingLebanon(List<DocumentModel> filesUploaded, string billType, int currentUserId)
        {
            IFormatProvider enUsDateFormat = new CultureInfo("en-US").DateTimeFormat;
            List<Bill> bills = new List<Bill>();
            List<BillDetails> billsDetails = new List<BillDetails>();
            int phoneBookId = 0;
            string dialedNumber = string.Empty;
            int TypePhoneNumberId = 0;
            int serviceTypeId = 0;
            bool isServiceNeedApproved = false;
            bool nonOfficial = false;
            int operatrId = 0;
            string operatorKey = string.Empty;
            string dataUsage = string.Empty;
            string callDuration = string.Empty;
            string userPhoneNumber = string.Empty;
            int userId = 0;

            
                foreach (DocumentModel file in filesUploaded)
                {
                    using (var stream = System.IO.File.Open(file.URL, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {

                            DataSet datasetBills = reader.AsDataSet();
                            DataTable dataTable = datasetBills.Tables[0];

                            dataTable = dataTable.AsEnumerable()
                                                 .GroupBy(r => r.Field<object>("Column0"))
                                                 .SelectMany(x => x)
                                                 .CopyToDataTable();



                            for (int index = 0; index < dataTable.Rows.Count; index++)
                            {

                                if (Convert.ToString(dataTable.Rows[index]["Column0"]).Trim().Contains("SUBNO"))
                                {
                                    continue;
                                }

                                //check if this user is Exist or not
                                userPhoneNumber = LebanonUtilites.GetNumberWithoutCountryKey(Convert.ToString(dataTable.Rows[index]["Column0"]).Trim());
                                if (userId == 0)
                                {
                                    if (_unitOfWork.SecurityRepository.IsUserExistsByPhoneNumber(userPhoneNumber).Result)
                                    {
                                        userId = _unitOfWork.SecurityRepository.SearchByPhoneNumber(userPhoneNumber).Result;
                                    }
                                    else
                                    {
                                        userId = _unitOfWork.SecurityRepository.CreateUserUsingPhoneNumber(userPhoneNumber, (int)SimType.Voice, (int)SimProfileType.Activated).Result;
                                    }
                                }

                                //check if current row for dialed number(DESTINATION_NUMBER) column is number
                                if (LebanonUtilites.IsStringNumber(Convert.ToString(dataTable.Rows[index]["Column3"]).Trim()))
                                {
                                    dialedNumber = LebanonUtilites.GetNumberWithoutCountryKey(Convert.ToString(dataTable.Rows[index]["Column3"]).Trim());
                                    PhoneBook phoneBook = _unitOfWork.GetRepository<PhoneBook>().FirstOrDefault(x => x.PhoneNumber == dialedNumber);

                                    if (phoneBook != null)
                                    {
                                        phoneBookId = phoneBook.Id;
                                        TypePhoneNumberId = phoneBook.TypePhoneNumberId;

                                        operatorKey = LebanonUtilites.GetOperatorKeyFromNumber(Convert.ToString(dataTable.Rows[index]["Column3"]).Trim());
                                        Operator operatr = _unitOfWork.GetRepository<Operator>().FirstOrDefault(x => x.OperatorKey == Convert.ToInt64(operatorKey));
                                        operatrId = operatr != null ? operatr.Id : 0;
                                    }
                                    else
                                    {
                                        phoneBookId = 0;
                                        TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                        operatrId = 0;
                                    }
                                }
                                else
                                {
                                    dialedNumber = Convert.ToString(dataTable.Rows[index]["Column3"]).Trim();
                                    phoneBookId = 0;
                                    TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                    operatrId = 0;
                                }


                                //check on service type (CHARGETYPE)
                                ServiceUsed serviceUsed = _unitOfWork.GetRepository<ServiceUsed>().FirstOrDefault(x => x.ServiceUsedNameAr == Convert.ToString(dataTable.Rows[index]["Column5"]).Trim() || x.ServiceUsedNameEn == Convert.ToString(dataTable.Rows[index]["Column5"]).Trim());
                                if (serviceUsed != null)
                                {
                                    serviceTypeId = serviceUsed.Id;
                                    isServiceNeedApproved = serviceUsed.IsNeedApproved;
                                    nonOfficial = serviceUsed.NonOfficial;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(dataTable.Rows[index]["Column5"]).Trim()))
                                    {
                                        //adding a new service type if type Not Exist
                                        ServiceUsed addingServiceUsed = new ServiceUsed
                                        {
                                            ServiceUsedNameAr = Convert.ToString(dataTable.Rows[index]["Column5"]).Trim(),
                                            ServiceUsedNameEn = Convert.ToString(dataTable.Rows[index]["Column5"]).Trim(),
                                            IsCalculatedValue = true,
                                            IsNeedApproved = false,
                                            NonOfficial = false
                                        };

                                        _unitOfWork.GetRepository<ServiceUsed>().Insert(addingServiceUsed);

                                        if (!_unitOfWork.SaveChanges())
                                        {
                                            throw new Exception("Error occurred when inserting a new service");
                                        }

                                        serviceTypeId = addingServiceUsed.Id;
                                        isServiceNeedApproved = addingServiceUsed.IsNeedApproved;
                                        nonOfficial = addingServiceUsed.NonOfficial;
                                    }
                                }

                                //when this service is INTERNET
                                if (Convert.ToString(dataTable.Rows[index]["Column5"]).Trim().Contains("Data"))
                                {
                                    dataUsage = Convert.ToString(dataTable.Rows[index]["Column4"]).Trim();
                                    callDuration = "-";
                                }
                                else
                                {
                                    dataUsage = "-";
                                    callDuration = LebanonUtilites.GetMinutesFromDateTime(Convert.ToString(dataTable.Rows[index]["Column4"]).Trim());
                                }

                                //chicking if current bill is Exist
                                if (!IsBillExist(LebanonUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)), userId))
                                {
                                    billsDetails.Add(new BillDetails
                                    {
                                        PhoneBookId = phoneBookId,
                                        CallDuration = callDuration,
                                        CallDateTime = LebanonUtilites.GetCallDateTime(dataTable.Rows[index]["Column1"].ToString(), dataTable.Rows[index]["Column2"].ToString()),
                                        CallNetPrice = (billType == "Calls") ? Convert.ToDecimal(dataTable.Rows[index]["Column7"]) : Convert.ToDecimal(dataTable.Rows[index]["Column6"]),
                                        CallRetailPrice = (billType == "Calls") ? Convert.ToDecimal(dataTable.Rows[index]["Column7"]) : Convert.ToDecimal(dataTable.Rows[index]["Column6"]),
                                        PhoneNumber = dialedNumber,
                                        TypePhoneNumberId = TypePhoneNumberId,
                                        ServiceUsedId = serviceTypeId,
                                        IsServiceUsedNeedApproved = isServiceNeedApproved,
                                        TypeServiceUsedId = (nonOfficial) ? (int)TypesPhoneNumber.Personal : (int)TypesPhoneNumber.Unknown,
                                        StatusServiceUsedId = (int)SystemEnum.StatusCycleBills.Upload,
                                        OperatorId = operatrId,
                                        DataUsage = dataUsage,
                                    });
                                }
                                else
                                {
                                    //added this because lebanon unicef have multi file 
                                    BillModel billModel = GetBill(LebanonUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)), userId);

                                    if (!IsBillDetailsExist(dialedNumber, LebanonUtilites.GetCallDateTime(dataTable.Rows[index]["Column1"].ToString(), dataTable.Rows[index]["Column2"].ToString()), billModel.Id))
                                    {
                                        billsDetails.Add(new BillDetails
                                        {
                                            PhoneBookId = phoneBookId,
                                            CallDuration = callDuration,
                                            CallDateTime = LebanonUtilites.GetCallDateTime(dataTable.Rows[index]["Column1"].ToString(), dataTable.Rows[index]["Column2"].ToString()),
                                            CallNetPrice = (billType == "Calls") ? Convert.ToDecimal(dataTable.Rows[index]["Column7"]) : Convert.ToDecimal(dataTable.Rows[index]["Column6"]),
                                            CallRetailPrice = (billType == "Calls") ? Convert.ToDecimal(dataTable.Rows[index]["Column7"]) : Convert.ToDecimal(dataTable.Rows[index]["Column6"]),
                                            PhoneNumber = dialedNumber,
                                            TypePhoneNumberId = TypePhoneNumberId,
                                            ServiceUsedId = serviceTypeId,
                                            IsServiceUsedNeedApproved = isServiceNeedApproved,
                                            TypeServiceUsedId = (nonOfficial) ? (int)TypesPhoneNumber.Personal : (int)TypesPhoneNumber.Unknown,
                                            StatusServiceUsedId = (int)SystemEnum.StatusCycleBills.Upload,
                                            OperatorId = operatrId,
                                            DataUsage = dataUsage,
                                            BillId = billModel.Id
                                        });
                                    }
                                }

                                //To avoid error index out of range... when read last row
                                if (index + 1 < dataTable.Rows.Count)
                                {
                                    // Adding this condition to check if next row is a new number,
                                    if (LebanonUtilites.GetNumberWithoutCountryKey(dataTable.Rows[index]["Column0"].ToString().Trim()) != LebanonUtilites.GetNumberWithoutCountryKey(dataTable.Rows[index + 1]["Column0"].ToString().Trim()))
                                    {
                                        //chicking if current bill is Exist
                                        if (!IsBillExist(LebanonUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)), userId))
                                        {
                                            bills.Add(new Bill
                                            {
                                                UserId = userId,
                                                BillDate = LebanonUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)),
                                                SubmittedByAdmin = false,
                                                SubmittedByUser = false,
                                                IsPaid = false,
                                                IsTerminal = false,
                                                StatusBillId = (int)SystemEnum.StatusCycleBills.Upload,
                                                BillDetails = billsDetails
                                            });
                                        }
                                        else
                                        {
                                            BillModel billModel = GetBill(LebanonUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)), userId);

                                        if(billsDetails.Count > 0) { 
                                            _unitOfWork.GetRepository<BillDetails>().InsertRange(billsDetails);
                                            _unitOfWork.SaveChanges();
                                            }
                                        }
                                        //this means the next row contains a new  Number, Must reset bills details list
                                        billsDetails = new List<BillDetails>();
                                        userId = 0;
                                    }
                                }
                                else
                                {
                                    //chicking if current bill is added
                                    if (!IsBillExist(LebanonUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)), userId))
                                    {
                                        bills.Add(new Bill
                                        {
                                            UserId = userId,
                                            BillDate = LebanonUtilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column1"], enUsDateFormat)),
                                            SubmittedByAdmin = false,
                                            SubmittedByUser = false,
                                            IsPaid = false,
                                            IsTerminal = false,
                                            StatusBillId = (int)SystemEnum.StatusCycleBills.Upload,
                                            BillDetails = billsDetails
                                        });
                                    }
                                    //this means the next row contains a new  Number, Must reset bills details list
                                    billsDetails = new List<BillDetails>();
                                    userId = 0;
                                }
                            }
                        }
                    }
                }

                _unitOfWork.GetRepository<Bill>().InsertRange(bills);
                _unitOfWork.SaveChanges();

                return true;
           
        }

        public bool UploadDataRoamingLebanon(List<DocumentModel> filesUploaded, int currentUserId)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UploadDataLebanon(List<DocumentModel> filesUploaded, int currentUserId)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<string>> ReminderUsersAddedBills()
        {
            try
            {
                var usersUplodedBills = new Tuple<List<string>, List<NotificationItemModel>, List<NotificationItemModel>, List<NotificationItemModel>>(null,null,null,null);
                List<string> usersIdsBillUploded = new List<string>();
                bool isReminderBySyatem = _unitOfWork.GeneralSettingsRepository.IsReminderBySystem();

                usersUplodedBills = await _unitOfWork.BillsRepository.GetbillsGreaterThanServicesPrices();

                if (isReminderBySyatem)
                {
                    //add notification new services added
                    foreach (var notification in usersUplodedBills.Item2)
                    {
                        _unitOfWork.NotificationRepository.AddNotificationItem(notification);
                    }

                    //add notification removed services
                    foreach (var notification in usersUplodedBills.Item3)
                    {
                        _unitOfWork.NotificationRepository.AddNotificationItem(notification);
                    }

                    //add notification services Grater Than Plan Servies
                    foreach (var notification in usersUplodedBills.Item4)
                    {
                        _unitOfWork.NotificationRepository.AddNotificationItem(notification);
                    }
                }

                if (_unitOfWork.GeneralSettingsRepository.IsReminderByEmail())
                {
                    foreach (string user in usersUplodedBills.Item1)
                    {
                      await  _unitOfWork.EmailRepository.ImportBillEmail(_unitOfWork.SecurityRepository.GetEmailByUserId(Convert.ToInt32(user)).Result);
                    }
                }
                return (isReminderBySyatem) ? usersUplodedBills.Item1 : new List<string>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Sheard ...

        private bool IsBillExist(DateTime billDateTime, int userId)
        {
            try
            {
                Bill bill = _unitOfWork.GetRepository<Bill>().FirstOrDefault(x => x.BillDate == billDateTime && x.UserId == userId);

                return (bill != null) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private BillModel GetBill(DateTime billDateTime, int userId)
        {
            try
            {
                Bill bill = _unitOfWork.GetRepository<Bill>().FirstOrDefault(x => x.BillDate == billDateTime && x.UserId == userId);

                return _mapper.Map<BillModel>(bill);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsBillDetailsExist(string dialedNumber, DateTime callDateTime, int billId)
        {
            try
            {
                BillDetails bill = _unitOfWork.GetRepository<BillDetails>().FirstOrDefault(x => x.BillId == billId && x.CallDateTime == callDateTime && x.PhoneNumber == dialedNumber);

                return (bill != null) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
