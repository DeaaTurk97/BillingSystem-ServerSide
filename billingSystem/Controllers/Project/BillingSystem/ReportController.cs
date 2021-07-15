using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Services;
using AspNetCore.Reporting;
using billingSystem.ReportFiles.Dataset;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static Acorna.Core.DTOs.SystemEnum;

namespace billingSystem.Controllers.Project.BillingSystem
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IUnitOfWorkService unitOfWorkService,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWorkService = unitOfWorkService;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

		[HttpPost]
        [Route("CallSummaryReport")]
        public ActionResult GetCallSummaryReportFile(CallsInfoFilterModel model)
		{
            var reportName = ReportNames.CallSummary.ToString();
            model.PageIndex = 1;
            model.PageSize = 100000;
            var returnString = GenerateCallDetailsReport(model, reportName);
			return File(returnString, System.Net.Mime.MediaTypeNames.Application.Octet, reportName + DateTime.Now.ToString("ddMMyyyyHHmmss") + "." + model.ReportType);
		}


        public byte[] GenerateCallDetailsReport(CallsInfoFilterModel model, string reportName)
        {
            var report = getLocalReport(reportName);

            var list = _unitOfWorkService.CallDetailsViewService.GetCallSummary(model);
            report.AddDataSource("ReportDataSet", list.DataRecord);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var result = report.Execute(GetRenderType(model.ReportType), 1, parameters);
            return result.MainStream;
        }

        private LocalReport getLocalReport(string reportName)
		{
            string webRootPath = _webHostEnvironment.ContentRootPath;
            string rdlcFilePath = string.Format("{0}\\ReportFiles\\{1}.rdlc", webRootPath, reportName);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");
            LocalReport report = new LocalReport(rdlcFilePath);

            return report;
        }

        private RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;
            switch (reportType.ToLower())
            {
                default:
                case "pdf":
                    renderType = RenderType.Pdf;
                    break;
                case "docx":
                    renderType = RenderType.Word;
                    break;
                case "xlsx":
                    renderType = RenderType.Excel;
                    break;
            }

            return renderType;
        }
    }
}
