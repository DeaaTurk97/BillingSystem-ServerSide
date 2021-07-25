using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IBillsDetailsService
    {
        Task<PaginationRecord<UnDefinedNumbersDTO>> GetAllUndefinedNumbers(int billId);
        bool DefinitionNewNumbers(List<UnDefinedNumbersDTO> phoneNumbers, int currentUserId);
    }
}
