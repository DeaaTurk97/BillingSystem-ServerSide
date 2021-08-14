using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IBillsSummaryRepository : IRepository<Bill>
    {
        Task<PaginationRecord<BillsSummaryDTO>> GetBillsSummary(int pageIndex, int pageSize, int currentUserId, string currentUserRole);
        Task<BillsSummaryDTO> GetBillSummaryById(int billId);
    }
}
