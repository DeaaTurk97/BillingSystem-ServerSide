using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IBillsSummaryService
    {
        Task<PaginationRecord<BillsSummaryModel>> GetBillsSummary(int pageIndex, int pageSize, int currentUserId);
        bool UpdatePayBill(int billId, bool isPaid);
    }
}
