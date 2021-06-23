using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem.Report;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem.Report
{
	public interface ICallDetailsReportService
    {
        Task<List<CallDetailsReportDTO>> GetReport(CallDetailsReportModel filter);
    }
}
