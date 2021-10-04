using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    public class SimCardTypeService : ISimCardTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SimCardTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<SimCardTypeModel>> GetAllSimCardType()
        {
            try
            {
                List<SimCardType> simCardTypes = await _unitOfWork.GetRepository<SimCardType>().GetAllAsync();

                return _mapper.Map<List<SimCardTypeModel>>(simCardTypes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
