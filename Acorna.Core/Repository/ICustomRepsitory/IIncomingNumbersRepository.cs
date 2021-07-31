using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IIncomingNumbersRepository
    {
        Task<PaginationRecord<PhoneBookModel>> GetAllIncomingNumbers(int pageIndex, int pageSize, int statusNumber);
        Task<PaginationRecord<PhoneBookModel>> GetIncomingNumbersByGroupId(int pageIndex, int pageSize, int statusNumber, int userId);
    }
}
