using Acorna.Core.Entity.Notification;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Notification;
using Acorna.Core.Models.Project.BillingSystem.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.Project.BillingSystem.Report
{
    public interface ICallDetailsReportRepository 
    {
        List<CallDetailsDTO> GetCallDetails(CallsInfoFilterModel filter, out int countRecord);
        List<CallSummaryDTO> GetCallSummary(CallsInfoFilterModel filter, out int countRecord);
        List<CallFinanceDTO> GetCallFinance(CallsInfoFilterModel filter, out int countRecord);
    }
}
