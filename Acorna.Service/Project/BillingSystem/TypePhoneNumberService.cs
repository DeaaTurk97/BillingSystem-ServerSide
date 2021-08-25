using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Service.Project.BillingSystem
{
    public class TypePhoneNumberService : ITypePhoneNumberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TypePhoneNumberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TypePhoneNumberModel>> GetAllTypesPhoneNumber()
        {
            try
            {
                List<TypePhoneNumber> typePhoneNumbers = await _unitOfWork.GetRepository<TypePhoneNumber>().GetAllAsync();
                return _mapper.Map<List<TypePhoneNumberModel>>(typePhoneNumbers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TypePhoneNumberModel>> GetAllTypesWithoutFreeType()
        {
            try
            {
                List<TypePhoneNumber> typePhoneNumbers = await _unitOfWork.GetRepository<TypePhoneNumber>().GetAllAsync();

                if (_unitOfWork.GeneralSettingsRepository.IsDeleteFreeTypeNumber())
                {
                    int indexFreeType = typePhoneNumbers.FindIndex(x => x.Id == (int)TypesPhoneNumber.Free);
                    typePhoneNumbers.RemoveAt(indexFreeType);
                }

                return _mapper.Map<List<TypePhoneNumberModel>>(typePhoneNumbers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
