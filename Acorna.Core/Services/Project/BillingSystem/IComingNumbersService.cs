using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IComingNumbersService
    {
        Task<PaginationRecord<PhoneBookModel>> GetComingNumbers(int pageIndex, int pageSize, int statusNumber, int currentUserId, string currentUserRole);
        bool ApprovePhoneNumbers(List<int> phoneNumberId, int currentUserId);
        bool InprogressPhoneNumbers(List<int> phoneNumberId, int currentUserId);
        bool RejectPhoneNumbers(List<int> phoneNumberId, int currentUserId);
    }
}
