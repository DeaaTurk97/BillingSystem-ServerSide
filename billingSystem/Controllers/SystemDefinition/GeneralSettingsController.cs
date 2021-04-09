using Acorna.Controllers.Base;
using Acorna.Core.IServices.Project;
using Acorna.Core.Models.SystemDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Controllers.SystemDefinition
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralSettingsController : TeamControllerBase
    {
        private readonly IGeneralSettingsService _generalSettingsService;

        public GeneralSettingsController(IGeneralSettingsService generalSettingsService)
        {
            _generalSettingsService = generalSettingsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetGeneralSettings()
        {
            try
            {
                return Ok(await _generalSettingsService.GetAllAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] List<GeneralSettingModel> generalSettingModel)
        {
            try
            {
                await _generalSettingsService.UpdateGeneralSettingsAsync(generalSettingModel);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }

            return Ok(true);
        }
    }
}
