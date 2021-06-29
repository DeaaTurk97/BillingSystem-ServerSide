using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IServiceType
    {
        Task<List<ServiceTypeModel>> GetAllServicesTypes();
        Task<PaginationRecord<ServiceTypeModel>> GetServicesTypes(int pageIndex, int pageSize);
        Task<ServiceTypeModel> GetServiceTypeId(int serviceTypeId);
        int AddServiceType(ServiceTypeModel serviceTypeModel);
        bool UpdateServiceType(ServiceTypeModel serviceTypeModel);
        bool DeleteServiceType(int id);
    }
}
