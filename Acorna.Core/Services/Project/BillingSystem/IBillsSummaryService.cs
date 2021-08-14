using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IBillsSummaryService
    {
        Task<PaginationRecord<BillsSummaryDTO>> GetBillsSummary(int pageIndex, int pageSize, int currentUserId, string currentUserRole);
        bool UpdatePayBill(int billId, bool isPaid);
        Task<BillsSummaryModel> GetBillSummaryById(int billId);
    }
}
