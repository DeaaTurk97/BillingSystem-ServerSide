using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IGovernorateService
    {
        Task<List<GovernorateModel>> GetAllGovernorates();
        Task<PaginationRecord<GovernorateModel>> GetGovernorates(int pageIndex, int pageSize);
        Task<GovernorateModel> GetGovernorateId(int governorateId);
        int AddGovernorate(GovernorateModel governorateModel);
        bool UpdateGovernorate(GovernorateModel governorateModel);
        bool DeleteGovernorate(int id);
    }
}
