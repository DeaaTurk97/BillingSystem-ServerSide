using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IServiceUsed
    {
        Task<List<ServiceUsedModel>> GetAllServicesUsed();
        Task<PaginationRecord<ServiceUsedModel>> GetServicesUsed(int pageIndex, int pageSize);
        Task<ServiceUsedModel> GetServiceUsedId(int serviceTypeId);
        int AddServiceUsed(ServiceUsedModel serviceTypeModel);
        bool UpdateServiceUsed(ServiceUsedModel serviceTypeModel);
        bool DeleteServiceUsed(int id);
    }
}
