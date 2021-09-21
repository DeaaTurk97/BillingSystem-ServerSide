using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IComingServicesService
    {
        Task<PaginationRecord<ServicesNeedApprovedDTO>> GetComingServices(int pageIndex, int pageSize, int statusNumber, int currentUserId, string currentUserRole);
        List<string> ApproveServices(List<int> servicesId, int currentUserId);
        List<string> InprogressServices(List<int> servicesId, int currentUserId);
        List<string> RejectServices(List<int> servicesId, int currentUserId);
    }
}
