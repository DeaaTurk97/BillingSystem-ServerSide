using Acorna.Core.DTOs.billingSystem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IIncomingNumbersRepository
    {
        Task<List<PhoneBookDTO>> GetAllIncomingNumbers(int pageIndex, int pageSize, int statusNumber);
        Task<List<PhoneBookDTO>> GetIncomingNumbersByGroupId(int pageIndex, int pageSize, int statusNumber, int userId);
    }
}
