using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IBillsDetailsRepository : IRepository<BillDetails>
    {
        Task<PaginationRecord<UnDefinedNumbersDTO>> GetAllUndefinedNumbers(int billId);
        Task<PaginationRecord<ServicesNeedApprovedDTO>> GetServicesNeedApproval(int billId);
    }
}
