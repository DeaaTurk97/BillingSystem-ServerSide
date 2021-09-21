using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IBillsDetailsService
    {
        Task<PaginationRecord<UnDefinedNumbersDTO>> GetAllUndefinedNumbers(int billId);
        List<string> DefinitionNewNumbers(List<UnDefinedNumbersDTO> phoneNumbers, string billId, int currentUserId);
        List<string> ServicesSubmitted(List<ServicesNeedApprovedDTO> servicesNeedApproved, string billId, int currentUserId);
        List<string> UpdateSubmitBill(int billId);
        Task<PaginationRecord<ServicesNeedApprovedDTO>> GetServicesNeedApproval(int billId);
    }
}
