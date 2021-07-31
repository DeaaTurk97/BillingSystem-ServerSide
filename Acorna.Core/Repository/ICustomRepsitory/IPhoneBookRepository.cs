using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IPhoneBookRepository : IRepository<PhoneBook>
    {
        Task<PaginationRecord<PhoneBookModel>> GetOfficialPhonesBook(int pageIndex, int pageSize);
        Task<PaginationRecord<PhoneBookModel>> GetPhonesBookByGroupId(int pageIndex, int pageSize, int userId);
        Task<PaginationRecord<PhoneBookModel>> GetPhonesBookByUserId(int pageIndex, int pageSize, int UserId);
    }
}
