using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
