using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Sheard;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IComingBillsRepository
    {
        Task<PaginationRecord<BillsSummaryDTO>> GetAllComingBills(int pageIndex, int pageSize, int statusNumber);
        Task<PaginationRecord<BillsSummaryDTO>> GetComingBillsByGroupId(int pageIndex, int pageSize, int statusNumber, int userId);
        Task<PaginationRecord<BillsSummaryDTO>> GetComingBillsFinance(int pageIndex, int pageSize);
    }
}
