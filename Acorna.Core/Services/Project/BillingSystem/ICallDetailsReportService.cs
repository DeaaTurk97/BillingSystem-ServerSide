using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
	public interface ICallDetailsViewService
    {
        PaginationRecord<CallDetailsDTO> GetCallDetails(CallsInfoFilterModel filter);
        PaginationRecord<CallSummaryDTO> GetCallSummary(CallsInfoFilterModel filter);
        PaginationRecord<CallFinanceDTO> GetCallFinance(CallsInfoFilterModel filter);
    }
}
