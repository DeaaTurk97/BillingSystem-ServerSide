using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Core.Sheard;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Service.Project.BillingSystem
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhoneBookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PhoneBookModel>> GetAllPhonesBook()
        {
            try
            {
                List<PhoneBook> phoneBook = await _unitOfWork.GetRepository<PhoneBook>().GetAllAsync();
                return _mapper.Map<List<PhoneBookModel>>(phoneBook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginationRecord<PhoneBookModel>> GetPhonesBook(int pageIndex, int pageSize, int currentUserId, string currentUserRole)
        {
            RolesType rolesType = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);
            List<PhoneBookDTO> PhoneBookDTO = new List<PhoneBookDTO>();

            try
            {
                if (rolesType == RolesType.SuperAdmin || rolesType == RolesType.Admin)
                {
                    PhoneBookDTO = await _unitOfWork.PhoneBookRepository.GetOfficialPhonesBook(pageIndex, pageSize);
                }
                else if (rolesType == RolesType.AdminGroup)
                {
                    PhoneBookDTO = await _unitOfWork.PhoneBookRepository.GetPhonesBookByGroupId(pageIndex, pageSize, currentUserId);
                }
                else if (rolesType == RolesType.Employee)
                {
                    PhoneBookDTO = await _unitOfWork.PhoneBookRepository.GetPhonesBookByUserId(pageIndex, pageSize, currentUserId);
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

        public async Task<PhoneBookModel> GetPhoneBookId(int phoneBookId)
        {
            try
            {
                PhoneBook phoneBook = await _unitOfWork.GetRepository<PhoneBook>().GetSingleAsync(phoneBookId);
                return _mapper.Map<PhoneBookModel>(phoneBook);
            }
            catch (Exception)
            {
                throw;
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

        public int AddPhoneBook(PhoneBookModel phoneBookModel, int currentUserId, string currentUserRole)
        {
            RolesType rolesType = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);

            try
            {
                if (rolesType == RolesType.SuperAdmin || rolesType == RolesType.Admin || rolesType == RolesType.AdminGroup)
                {
                    phoneBookModel.StatusNumberId = (int)StatusCycleBills.Approved;
                    phoneBookModel.TypePhoneNumberId = (int)TypesPhoneNumber.Official;
                    phoneBookModel.StatusAdminId = currentUserId;
                }
                else if (rolesType == RolesType.Employee)
                {
                    phoneBookModel.PersonalUserId = currentUserId;
                    phoneBookModel.StatusNumberId = (int)StatusCycleBills.Approved;
                    phoneBookModel.TypePhoneNumberId = (int)TypesPhoneNumber.Personal;
                }

                PhoneBook phoneBook = _mapper.Map<PhoneBook>(phoneBookModel);

                if (phoneBook != null)
                {
                    _unitOfWork.GetRepository<PhoneBook>().Insert(phoneBook);
                    _unitOfWork.SaveChanges();
                }

                return phoneBook.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsNumberAdded(string phoneNumber, int currentUserId, string currentUserRole)
        {
            try
            {
                RolesType rolesType = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);
                List<PhoneBook> phoneBook = new List<PhoneBook>();

                if (rolesType == RolesType.SuperAdmin || rolesType == RolesType.Admin || rolesType == RolesType.AdminGroup)
                {
                    phoneBook = _unitOfWork.GetRepository<PhoneBook>().GetWhere(x => x.PhoneNumber == phoneNumber
                                                                                && x.TypePhoneNumberId == (int)TypesPhoneNumber.Official).ToList();
                }
                else if (rolesType == RolesType.Employee)
                {
                    phoneBook = _unitOfWork.GetRepository<PhoneBook>().GetWhere(x => x.PhoneNumber == phoneNumber
                                                                                && x.PersonalUserId == currentUserId
                                                                                && x.TypePhoneNumberId == (int)TypesPhoneNumber.Personal).ToList();
                }


                return  (phoneBook.Count > 0) ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool UpdatePhoneBook(PhoneBookModel phoneBookModel)
        {
            try
            {
                PhoneBook phoneBook = _unitOfWork.GetRepository<PhoneBook>().GetSingle(phoneBookModel.Id);


                if (phoneBook != null)
                {
                    phoneBook.PhoneName = phoneBookModel.PhoneName;
                    phoneBook.PhoneNumber = phoneBookModel.PhoneNumber;
                    phoneBook.StatusNumberId = phoneBookModel.StatusNumberId;
                    phoneBook.TypePhoneNumberId = phoneBookModel.TypePhoneNumberId;

                    _unitOfWork.GetRepository<PhoneBook>().Update(phoneBook);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePhoneBook(int id)
        {
            try
            {
                PhoneBook phoneBook = _unitOfWork.GetRepository<PhoneBook>().GetSingle(id);

                if (phoneBook != null)
                {
                    _unitOfWork.GetRepository<PhoneBook>().Delete(phoneBook);
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
