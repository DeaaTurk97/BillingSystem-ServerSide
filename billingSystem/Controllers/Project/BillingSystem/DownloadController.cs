using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem.Report;
using Acorna.Core.Services;
using AspNetCore.Reporting;
using billingSystem.ReportFiles.Dataset;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static Acorna.Core.DTOs.SystemEnum;


namespace billingSystem.Controllers.Project.BillingSystem
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadFileController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _context;
     
        public DownloadFileController(IUnitOfWorkService unitOfWorkService,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor context)
        {
            _unitOfWorkService = unitOfWorkService;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _context = context;
        }

        [HttpGet]
        [Route("Download")]
        public virtual ActionResult Download(string file)
        {
            var path = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "TempFiles", file);

            var contentType = System.Net.Mime.MediaTypeNames.Application.Octet;

            return PhysicalFile(path, contentType, file);
        }

    }
}
