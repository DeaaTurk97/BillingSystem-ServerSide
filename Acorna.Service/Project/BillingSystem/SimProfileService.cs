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
    public class SimProfileService : ISimProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SimProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<SimProfileModel>> GetAllSimProfile()
        {
            try
            {
                List<SimProfile> simProfiles = await _unitOfWork.GetRepository<SimProfile>().GetAllAsync();

                return _mapper.Map<List<SimProfileModel>>(simProfiles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
