using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IComingNumbersService
    {
        Task<PaginationRecord<PhoneBookModel>> GetComingNumbers(int pageIndex, int pageSize, int statusNumber, int currentUserId, string currentUserRole);
        List<string> ApprovePhoneNumbers(List<int> phoneNumberId, int currentUserId);
        List<string> InprogressPhoneNumbers(List<int> phoneNumberId, int currentUserId);
        List<string> RejectPhoneNumbers(List<int> phoneNumberId, int currentUserId);
    }
}
