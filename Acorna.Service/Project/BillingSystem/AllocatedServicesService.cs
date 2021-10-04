using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Services.Project.BillingSystem;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Service.Project.BillingSystem
{
    public class AllocatedServicesService : IAllocatedServicesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AllocatedServicesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<AllocatedUsersServiceModel> GetAllAlocatedServices(int userId)
        {
            try
            {
                List<AllocatedUsersService> allocatedUsersServices = _unitOfWork.GetRepository<AllocatedUsersService>()
                                                                                 .GetAllIncludingWithPredicate(new Expression<Func<AllocatedUsersService, object>>[] { x => x.User, y => y.ServiceUsed }
                                                                                 , new Expression<Func<AllocatedUsersService, bool>>[] { x => x.User.Id == userId });

                return _mapper.Map<List<AllocatedUsersServiceModel>>(allocatedUsersServices);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
