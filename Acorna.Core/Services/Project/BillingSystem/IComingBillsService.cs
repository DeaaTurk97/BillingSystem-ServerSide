using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IComingBillsService
    {
        Task<PaginationRecord<BillsSummaryDTO>> GetComingBills(int pageIndex, int pageSize, int statusNumber, int currentUserId, string currentUserRole);
        List<string> ApproveBills(List<int> billId);
        List<string> InprogressBills(List<int> billId);
        List<string> RejectBills(List<int> billId);
        List<string> PayBills(List<int> billId);
    }
}
