using Acorna.Core.Models.Project.BillingSystem.Report;

namespace Acorna.Core.Services.Project.BillingSystem
{
	public interface IReportService
    {
        byte[] GenerateCallSummaryReport(CallsInfoFilterModel model, string rootPath, string reportName);

        byte[] GenerateCallDetailsReport(CallsInfoFilterModel model, string rootPath, string reportName);

        byte[] GenerateCallFinanceReport(CallsInfoFilterModel model, string rootPath, string reportName);

        string GetReportUrl(byte[] reportByte, string rootPath, string reportName, string reportType, string scheme, string host, bool deleteOldTempFiles = true);
    }
}
