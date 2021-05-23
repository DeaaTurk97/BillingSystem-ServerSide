using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Service.Project.BillingSystem
{
    internal class ComingNumbersService : IComingNumbersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        internal ComingNumbersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationRecord<PhoneBookModel>> GetComingNumbers(int pageIndex, int pageSize, int statusNumber, int currentUserId, string currentUserRole)
        {
            RolesType rolesType = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);
            List<PhoneBookDTO> PhoneBookDTO = new List<PhoneBookDTO>();

            try
            {
                if (rolesType == RolesType.SuperAdmin || rolesType == RolesType.Admin)
                {
                    PhoneBookDTO = await _unitOfWork.IncomingNumbersRepository.GetAllIncomingNumbers(pageIndex, pageSize, statusNumber);
                }
                else if (rolesType == RolesType.AdminGroup)
                {
                    PhoneBookDTO = await _unitOfWork.IncomingNumbersRepository.GetIncomingNumbersByGroupId(pageIndex, pageSize, statusNumber, currentUserId);
                }

                PaginationRecord<PhoneBookModel> paginationRecordModel = new PaginationRecord<PhoneBookModel>
                {
                    DataRecord = _mapper.Map<List<PhoneBookModel>>(PhoneBookDTO),
                    CountRecord = GetCountRecord(),
                };


                return paginationRecordModel;
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

        public bool ApprovePhoneNumbers(List<int> phoneNumberId, int currentUserId)
        {
            try
            {
                PhoneBook phoneNumber = new PhoneBook();
                List<PhoneBook> approvePhoneNumbers = new List<PhoneBook>();

                phoneNumberId.ForEach(id =>
                {
                    phoneNumber = _unitOfWork.GetRepository<PhoneBook>().GetSingle(id);
                    phoneNumber.StatusNumberId = (int)StatusCycleBills.Approved;
                    phoneNumber.StatusAdminId = currentUserId;

                    approvePhoneNumbers.Add(phoneNumber);
                });

                if (approvePhoneNumbers.Count > 0)
                {
                    _unitOfWork.GetRepository<PhoneBook>().UpdateRange(approvePhoneNumbers);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InprogressPhoneNumbers(List<int> phoneNumberId, int currentUserId)
        {
            try
            {
                PhoneBook phoneNumber = new PhoneBook();
                List<PhoneBook> approvePhoneNumbers = new List<PhoneBook>();

                phoneNumberId.ForEach(id =>
                {
                    phoneNumber = _unitOfWork.GetRepository<PhoneBook>().GetSingle(id);
                    phoneNumber.StatusNumberId = (int)StatusCycleBills.InprogressToApproved;
                    phoneNumber.StatusAdminId = currentUserId;

                    approvePhoneNumbers.Add(phoneNumber);
                });

                if (approvePhoneNumbers.Count > 0)
                {
                    _unitOfWork.GetRepository<PhoneBook>().UpdateRange(approvePhoneNumbers);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RejectPhoneNumbers(List<int> phoneNumberId, int currentUserId)
        {
            try
            {
                PhoneBook phoneNumber = new PhoneBook();
                List<PhoneBook> approvePhoneNumbers = new List<PhoneBook>();

                phoneNumberId.ForEach(id =>
                {
                    phoneNumber = _unitOfWork.GetRepository<PhoneBook>().GetSingle(id);
                    phoneNumber.StatusNumberId = (int)StatusCycleBills.Rejected;
                    phoneNumber.StatusAdminId = currentUserId;

                    approvePhoneNumbers.Add(phoneNumber);
                });

                if (approvePhoneNumbers.Count > 0)
                {
                    _unitOfWork.GetRepository<PhoneBook>().UpdateRange(approvePhoneNumbers);
                    _unitOfWork.SaveChanges();
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
