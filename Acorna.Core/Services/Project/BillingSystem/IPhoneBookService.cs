using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IPhoneBookService
    {
        Task<List<PhoneBookModel>> GetAllPhonesBook();
        Task<PaginationRecord<PhoneBookModel>> GetPhonesBook(int pageIndex, int pageSize, int currentUserId, string currentUserRole);
        Task<PhoneBookModel> GetPhoneBookId(int groupId);
        int AddPhoneBook(PhoneBookModel groupModel, int currentUserId, string currentUserRole);
        bool UpdatePhoneBook(PhoneBookModel groupModel);
        bool DeletePhoneBook(int id);
        Task<bool> IsNumberAdded(string phoneNumber, int currentUserId, string currentUserRole);
    }
}
