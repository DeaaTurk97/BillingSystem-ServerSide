using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acorna.Controllers.Base;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace billingSystem.Controllers.Project.BillingSystem
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadBillsController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
       
        public UploadBillsController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
    }
}
