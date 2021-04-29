using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IPhoneBookRepository : IRepository<PhoneBook>
    {
        Task<List<PhoneBookDTO>> GetOfficialPhonesBook(int pageIndex, int pageSize);
        Task<List<PhoneBookDTO>> GetPhonesBookByGroupId(int pageIndex, int pageSize, int userId);
        Task<List<PhoneBookDTO>> GetPhonesBookByUserId(int pageIndex, int pageSize, int UserId);
    }
}
