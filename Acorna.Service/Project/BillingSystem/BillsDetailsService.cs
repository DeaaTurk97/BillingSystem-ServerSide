using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
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
    public class BillsDetailsService : IBillsDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BillsDetailsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public bool DefinitionNewNumbers(List<UnDefinedNumbersDTO> phoneNumbers, int currentUserId)
        {
            try
            {
                List<PhoneBook> phoneBookPrivate = new List<PhoneBook>();
                List<PhoneBook> phoneBookOfficial = new List<PhoneBook>();

                phoneNumbers.ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item.PhoneName) && item.TypePhoneNumberId != (int)TypesPhoneNumber.Personal && item.TypePhoneNumberId != (int)TypesPhoneNumber.Unknown)
                    {
                        phoneBookOfficial.Add(new PhoneBook
                        {
                            PhoneNumber = item.DialledNumber,
                            PhoneName = item.PhoneName,
                            TypePhoneNumberId = item.TypePhoneNumberId,
                            PersonalUserId = 0,
                            StatusNumberId = (int)StatusCycleBills.Available
                        });
                    }

                    if (!string.IsNullOrEmpty(item.PhoneName) && item.TypePhoneNumberId == (int)TypesPhoneNumber.Personal)
                    {
                        phoneBookPrivate.Add(new PhoneBook
                        {
                            PhoneNumber = item.DialledNumber,
                            PhoneName = item.PhoneName,
                            TypePhoneNumberId = item.TypePhoneNumberId,
                            PersonalUserId = currentUserId,
                            StatusNumberId = (int)StatusCycleBills.Available
                        });
                    }
                });

                if (phoneBookPrivate.Count > 0 || phoneBookOfficial.Count > 0)
                {
                    _unitOfWork.GetRepository<PhoneBook>().InsertRange(phoneBookPrivate);
                    _unitOfWork.GetRepository<PhoneBook>().InsertRange(phoneBookOfficial);

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
