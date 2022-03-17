using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IHttpContextAccessor _context;

        public ReportController(IUnitOfWorkService unitOfWorkService,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor context)
        {
            _unitOfWorkService = unitOfWorkService;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpPost]
        [Route("CallSummaryReport")]
        public ActionResult GetCallSummaryReport(CallsInfoFilterModel model)
        {
            try
            {
                var languageModel = _unitOfWorkService.SecurityService.GetLanguageInformations(CurrentUserId);
                model.Lang = languageModel.Result.LanguageCode.ToLower();

                var baseReportName = ReportNames.CallSummary.ToString();
                var reportName = model.Lang == Languages.ar.ToString() ? $"{baseReportName}Ar" : $"{baseReportName}En";

                //var reportName =  ReportNames.CallSummary.ToString();
                var rootPath = _webHostEnvironment.ContentRootPath;
                model.PageIndex = 1;
                model.PageSize = 100000;
                var reportByte = _unitOfWorkService.ReportService.GenerateCallSummaryReport(model, rootPath, reportName);

                var request = _context.HttpContext.Request;
                var scheme = request.Scheme;
                var host = request.Host.ToString();

                var urlPath = _unitOfWorkService.ReportService.GetReportUrl(reportByte, rootPath, reportName, model.ReportType, scheme, host);

                return Ok(new { urlPath });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("CallDetailsReport")]
        public ActionResult GetCallDetailsReport(CallsInfoFilterModel model)
        {
            try
            {
                var languageModel = _unitOfWorkService.SecurityService.GetLanguageInformations(CurrentUserId);
                model.Lang = languageModel.Result.LanguageCode.ToLower();

                var baseReportName = ReportNames.CallDetails.ToString();
                var reportName = model.Lang == Languages.ar.ToString() ? $"{baseReportName}Ar" : $"{baseReportName}En";

                //var reportName =  ReportNames.CallSummary.ToString();
                var rootPath = _webHostEnvironment.ContentRootPath;
                model.PageIndex = 1;
                model.PageSize = 100000;
                var reportByte = _unitOfWorkService.ReportService.GenerateCallDetailsReport(model, rootPath, reportName);

                var request = _context.HttpContext.Request;
                var scheme = request.Scheme;
                var host = request.Host.ToString();

                var urlPath = _unitOfWorkService.ReportService.GetReportUrl(reportByte, rootPath, reportName, model.ReportType, scheme, host);
                int ext = (int)(DateTime.Now.Ticks >> 10);
                return Ok(new { urlPath });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("CallFinanceReport")]
        public ActionResult GetCallFinanceReport(CallsInfoFilterModel model)
        {
            try
            {
                var languageModel = _unitOfWorkService.SecurityService.GetLanguageInformations(CurrentUserId);
                model.Lang = languageModel.Result.LanguageCode.ToLower();

                var baseReportName = ReportNames.CallFinance.ToString();
                var reportName = model.Lang == Languages.ar.ToString() ? $"{baseReportName}Ar" : $"{baseReportName}En";

                //var reportName =  ReportNames.CallSummary.ToString();
                var rootPath = _webHostEnvironment.ContentRootPath;
                model.PageIndex = 1;
                model.PageSize = 100000;
                var reportByte = _unitOfWorkService.ReportService.GenerateCallFinanceReport(model, rootPath, reportName);

                var request = _context.HttpContext.Request;
                var scheme = request.Scheme;
                var host = request.Host.ToString();

                var urlPath = _unitOfWorkService.ReportService.GetReportUrl(reportByte, rootPath, reportName, model.ReportType, scheme, host);

                return Ok(new { urlPath });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }

}
