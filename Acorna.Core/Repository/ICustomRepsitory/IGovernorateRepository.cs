using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IGovernorateRepository : IRepository<Governorate>
    {
        Task<IEnumerable<GovernorateDTO>> GetGovernorates(int pageIndex, int pageSize);
    }
}
