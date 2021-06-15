using Acorna.CommonMember;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using AutoMapper;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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

        public bool UploadMTNBills(List<DocumentModel> filesUploaded, int currentUserId)
        {
            try
            {
                List<Bill> bills = new List<Bill>();
                List<BillDetails> billsDetails = new List<BillDetails>();
                string phoneBookId = string.Empty;
                string dialedNumber = string.Empty;
                int TypePhoneNumberId = 0;
                int serviceTypeId = 0;
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
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet datasetBills = reader.AsDataSet();
                            DataTable dataTable = datasetBills.Tables[0];


                            dataTable = dataTable.AsEnumerable()
                                                 .GroupBy(r => r.Field<object>("Column5"))
                                                 .SelectMany(x => x)
                                                 .CopyToDataTable();

                            while (reader.Read()) //Each row of the file
                            {
                                if (reader.GetValue(0).ToString().Trim().Contains("customer_id"))
                                {
                                    continue;
                                }

                                //check if this user is Exist or not
                                userPhoneNumber = Utilites.GetNumberWithoutCountryKey(Convert.ToString(reader.GetValue(1)).Trim());
                                if (userId == 0)
                                {
                                    if (_unitOfWork.SecurityRepository.IsUserExistsByPhoneNumber(userPhoneNumber).Result)
                                    {
                                        userId = _unitOfWork.SecurityRepository.SearchByPhoneNumber(userPhoneNumber).Result;
                                    }
                                    else
                                    {
                                        userId = _unitOfWork.SecurityRepository.CreateUserUsingPhoneNumber(userPhoneNumber).Result;
                                    }
                                }

                                //check if current row for called_number column is number
                                if (Utilites.IsStringNumber(Convert.ToString(reader.GetValue(2)).Trim()))
                                {
                                    PhoneBook phoneBook = _unitOfWork.GetRepository<PhoneBook>().FirstOrDefault(x => x.PhoneNumber == Convert.ToString(reader.GetValue(2)));

                                    dialedNumber = Utilites.GetNumberWithoutCountryKey(Convert.ToString(reader.GetValue(2)).Trim());

                                    if (phoneBook != null)
                                    {
                                        phoneBookId = Convert.ToString(phoneBook.Id);
                                        TypePhoneNumberId = phoneBook.TypePhoneNumberId;

                                        operatorKey = Utilites.GetOperatorKeyFromNumber(Convert.ToString(reader.GetValue(2)).Trim());
                                        Operator operatr = _unitOfWork.GetRepository<Operator>().FirstOrDefault(x => x.OperatorKey == Convert.ToInt32(operatorKey));
                                        operatrId = operatr != null ? operatr.Id : 0;
                                    }
                                    else
                                    {
                                        phoneBookId = string.Empty;
                                        TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                        operatrId = 0;
                                    }
                                }
                                else
                                {
                                    dialedNumber = Convert.ToString(reader.GetValue(2)).Trim();
                                    phoneBookId = string.Empty;
                                    TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                    operatrId = 0;
                                }


                                if (!string.IsNullOrEmpty(Convert.ToString(reader.GetValue(6)).Trim()))
                                {
                                    ServiceType serviceType = _unitOfWork.GetRepository<ServiceType>().FirstOrDefault(x => x.ServiceNameAr == Convert.ToString(reader.GetValue(6)) || x.ServiceNameEn == Convert.ToString(reader.GetValue(6)));
                                    if (serviceType != null)
                                    {
                                        serviceTypeId = serviceType.Id;
                                    }
                                    else
                                    {
                                        //adding a new service type if type Not Exist
                                        ServiceType addingServiceType = new ServiceType
                                        {
                                            ServiceNameAr = Convert.ToString(reader.GetValue(6)).Trim(),
                                            ServiceNameEn = Convert.ToString(reader.GetValue(6)).Trim(),
                                            IsCalculatedValue = true
                                        };

                                        _unitOfWork.GetRepository<ServiceType>().Insert(addingServiceType);

                                        if (!_unitOfWork.SaveChanges())
                                        {
                                            throw new Exception("Error occurred when inserting a new service");
                                        }

                                        serviceTypeId = addingServiceType.Id;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToString(reader.GetValue(2)).Trim().Contains("bundle"))
                                    {
                                        ServiceType serviceBundle = _unitOfWork.GetRepository<ServiceType>().FirstOrDefault(x => x.ServiceNameAr == "EBU_حزمة" || x.ServiceNameEn == "Bundel");
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
                                    callDuration = Convert.ToString(reader.GetValue(8)).Trim();
                                    dataUsage = "-";
                                }

                                billsDetails.Add(new BillDetails
                                {
                                    PhoneBookId = phoneBookId,
                                    CallDateTime = Convert.ToDateTime(reader.GetValue(3)),
                                    CallDuration = callDuration,
                                    CallNetPrice = Convert.ToDecimal(reader.GetValue(9)),
                                    CallRetailPrice = Convert.ToDecimal(reader.GetValue(9)),
                                    PhoneNumber = dialedNumber,
                                    TypePhoneNumberId = TypePhoneNumberId,
                                    ServiceTypeId = serviceTypeId,
                                    OperatorId = operatrId,
                                    DataUsage = dataUsage,
                                });
                            }

                            if (billsDetails.Count > 0)
                            {
                                //chicking if current bill is added
                                if (!IsBillExist(Utilites.GetDateLastDayMonth(Convert.ToDateTime(reader.GetValue(3))), userId))
                                {
                                    bills.Add(new Bill
                                    {
                                        UserId = userId,
                                        BillDate = Utilites.GetDateLastDayMonth(Convert.ToDateTime(reader.GetValue(3))),
                                        SubmittedByAdmin = false,
                                        SubmittedByUser = false,
                                        IsPaid = false,
                                        IsTerminal = false,
                                        BillDetails = billsDetails
                                    });
                                }
                            }
                        }
                        // to reset userId after inserted bill
                        billsDetails = new List<BillDetails>();
                        userId = 0;
                    }
                }

                _unitOfWork.GetRepository<Bill>().InsertRange(bills);
                return _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UploadSyriaTelBills(List<DocumentModel> filesUploaded, int currentUserId)
        {
            List<Bill> bills = new List<Bill>();
            List<BillDetails> billsDetails = new List<BillDetails>();
            string phoneBookId = string.Empty;
            string dialedNumber = string.Empty;
            int TypePhoneNumberId = 0;
            int serviceTypeId = 0;
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
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {

                            DataSet datasetBills = reader.AsDataSet();
                            DataTable dataTable = datasetBills.Tables[0];


                            dataTable = dataTable.AsEnumerable()
                                                 .GroupBy(r => r.Field<object>("Column5"))
                                                 .SelectMany(x => x)
                                                 .CopyToDataTable();



                            for (int index = 0; index < dataTable.Rows.Count; index++)
                            {

                                if (Convert.ToString(dataTable.Rows[index]["Column0"]).Trim().Contains("التاريخ و الساعة"))
                                {
                                    continue;
                                }

                                //check if this user is Exist or not
                                userPhoneNumber = Utilites.GetNumberWithoutCountryKey(Convert.ToString(dataTable.Rows[index]["Column5"]).Trim());
                                if (userId == 0)
                                {
                                    if (_unitOfWork.SecurityRepository.IsUserExistsByPhoneNumber(userPhoneNumber).Result)
                                    {
                                        userId = _unitOfWork.SecurityRepository.SearchByPhoneNumber(userPhoneNumber).Result;
                                    }
                                    else
                                    {
                                        userId = _unitOfWork.SecurityRepository.CreateUserUsingPhoneNumber(userPhoneNumber).Result;
                                    }
                                }

                                //check if current row for called_number column is number
                                if (Utilites.IsStringNumber(Convert.ToString(dataTable.Rows[index]["Column1"]).Trim()))
                                {
                                    dialedNumber = Utilites.GetNumberWithoutCountryKey(Convert.ToString(dataTable.Rows[index]["Column1"]).Trim());
                                    PhoneBook phoneBook = _unitOfWork.GetRepository<PhoneBook>().FirstOrDefault(x => x.PhoneNumber == dialedNumber);

                                    if (phoneBook != null)
                                    {
                                        phoneBookId = Convert.ToString(phoneBook.Id);
                                        TypePhoneNumberId = phoneBook.TypePhoneNumberId;

                                        operatorKey = Utilites.GetOperatorKeyFromNumber(Convert.ToString(dataTable.Rows[index]["Column1"]).Trim());
                                        Operator operatr = _unitOfWork.GetRepository<Operator>().FirstOrDefault(x => x.OperatorKey == Convert.ToInt32(operatorKey));
                                        operatrId = operatr != null ? operatr.Id : 0;
                                    }
                                    else
                                    {
                                        phoneBookId = string.Empty;
                                        TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                        operatrId = 0;
                                    }
                                }
                                else
                                {
                                    dialedNumber = Convert.ToString(dataTable.Rows[index]["Column1"]).Trim();
                                    phoneBookId = string.Empty;
                                    TypePhoneNumberId = (int)TypesPhoneNumber.Unknown;
                                    operatrId = 0;
                                }

                                if (!string.IsNullOrEmpty(Convert.ToString(dataTable.Rows[index]["Column2"]).Trim()) && Convert.ToString(dataTable.Rows[index]["Column2"]).Trim() != "????")
                                {
                                    ServiceType serviceType = _unitOfWork.GetRepository<ServiceType>().FirstOrDefault(x => x.ServiceNameAr == Convert.ToString(dataTable.Rows[index]["Column2"]).Trim() || x.ServiceNameEn == Convert.ToString(dataTable.Rows[index]["Column2"]).Trim());
                                    if (serviceType != null)
                                    {
                                        serviceTypeId = serviceType.Id;
                                    }
                                    else
                                    {
                                        //adding a new service type if type Not Exist
                                        ServiceType addingServiceType = new ServiceType
                                        {
                                            ServiceNameAr = Convert.ToString(dataTable.Rows[index]["Column2"]).Trim(),
                                            ServiceNameEn = Convert.ToString(dataTable.Rows[index]["Column2"]).Trim(),
                                            IsCalculatedValue = true
                                        };

                                        _unitOfWork.GetRepository<ServiceType>().Insert(addingServiceType);

                                        if (!_unitOfWork.SaveChanges())
                                        {
                                            throw new Exception("Error occurred when inserting a new service");
                                        }

                                        serviceTypeId = addingServiceType.Id;
                                    }
                                }
                                else
                                {
                                    //service Number one is empty or ????
                                    serviceTypeId = 1;
                                }


                                //when this service is INTERNET
                                if (Convert.ToString(dataTable.Rows[index]["Column2"]).Trim().Contains("حزمة"))
                                {
                                    dataUsage = Convert.ToString(dataTable.Rows[index]["Column4"]).Trim();
                                    callDuration = "-";
                                }
                                else
                                {
                                    dataUsage = "-";
                                    callDuration = Convert.ToString(dataTable.Rows[index]["Column4"]).Trim();
                                }

                                billsDetails.Add(new BillDetails
                                {
                                    PhoneBookId = phoneBookId,
                                    CallDuration = callDuration,
                                    CallDateTime = Convert.ToDateTime(dataTable.Rows[index]["Column0"]),
                                    CallNetPrice = Convert.ToDecimal(dataTable.Rows[index]["Column3"]),
                                    CallRetailPrice = Convert.ToDecimal(dataTable.Rows[index]["Column3"]),
                                    PhoneNumber = dialedNumber,
                                    TypePhoneNumberId = TypePhoneNumberId,
                                    ServiceTypeId = serviceTypeId,
                                    OperatorId = operatrId,
                                    DataUsage = dataUsage,
                                });

                                //To avoid error index out of range... when read last row
                                if (index + 1 < dataTable.Rows.Count)
                                {
                                    // Adding this condition to check if next row is a new number,
                                    if (Utilites.GetNumberWithoutCountryKey(dataTable.Rows[index]["Column5"].ToString().Trim()) != Utilites.GetNumberWithoutCountryKey(dataTable.Rows[index + 1]["Column5"].ToString().Trim()))
                                    {
                                        //chicking if current bill is Exist
                                        if (!IsBillExist(Utilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column0"])), userId))
                                        {
                                            bills.Add(new Bill
                                            {
                                                UserId = userId,
                                                BillDate = Utilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column0"])),
                                                SubmittedByAdmin = false,
                                                SubmittedByUser = false,
                                                IsPaid = false,
                                                IsTerminal = false,
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
                                    if (!IsBillExist(Utilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column0"])), userId))
                                    {
                                        bills.Add(new Bill
                                        {
                                            UserId = userId,
                                            BillDate = Utilites.GetDateLastDayMonth(Convert.ToDateTime(dataTable.Rows[index]["Column0"])),
                                            SubmittedByAdmin = false,
                                            SubmittedByUser = false,
                                            IsPaid = false,
                                            IsTerminal = false,
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
                return _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
    }
}
