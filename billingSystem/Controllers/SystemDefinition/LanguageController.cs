using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Controllers.Base;
using Acorna.Core.IServices.SystemDefinition;
using Acorna.Core.Models.SystemDefinition;

namespace Acorna.Controllers.SystemDefinition
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : TeamControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        [HttpGet]
        [Route("GetAllLanguages")]
        public async Task<IActionResult> GetAllLanguages()
        {
            try
            {
                List<LanguageModel> languageModel = await _languageService.GetAllLanguagesAsync();
                return Ok(languageModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("GetLanguages")]
        public async Task<IActionResult> GetLanguages(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _languageService.GetAllLanguagesAsync(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetLanguageById")]
        public IActionResult GetLanguageById(int id)
        {
            try
            {
                return Ok(_languageService.GetLanguageById(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("AddLanguage")]
        public IActionResult AddLanguage([FromBody] LanguageModel languageModel)
        {
            try
            {
                return Ok(_languageService.AddLanguage(languageModel));
            }
            catch (Exception)
            {
                return BadRequest("false");
            }
        }

        [HttpPut]
        [Route("UpdateLanguage")]
        public async Task<IActionResult> UpdateLanguage(LanguageModel languageModel)
        {
            try
            {
                if (languageModel.Id != 0)
                    await _languageService.UpdateLanguageAsync(languageModel);
            }
            catch (Exception)
            {
                return BadRequest("false");
            }

            return Ok("true");
        }

        [HttpDelete]
        [Route("DeleteLanguage")]
        public IActionResult DeleteLanguage(int id)
        {
            try
            {
                _languageService.DeleteLanguage(id);
            }
            catch (Exception)
            {
                BadRequest("false");
            }

            return Ok("true");
        }
    }
}
