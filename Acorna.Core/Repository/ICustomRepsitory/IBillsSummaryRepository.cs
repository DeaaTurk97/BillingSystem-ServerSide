using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IBillsSummaryRepository : IRepository<Bill>
    {
        Task<IEnumerable<BillsSummaryDTO>> GetBillsSummary(int pageIndex, int pageSize, int currentUserId);
    }
}
