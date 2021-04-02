﻿using Acorna.Controllers.Base;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Services.Project.BillingSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace billingSystem.Controllers.Project.BillingSystem
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : TeamControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [Route("GetAllCountries")]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                return Ok(await _countryService.GetAllCountries());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetCountries")]
        public async Task<IActionResult> GetCountries(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _countryService.GetCountries(pageIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetCountryId")]
        public async Task<IActionResult> GetCountryId(int countryId)
        {
            try
            {
                return Ok(await _countryService.GetCountryId(countryId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddCountry")]
        public IActionResult AddCountry(CountryModel countryModel)
        {
            try
            {
                return Ok(_countryService.AddCountry(countryModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("UpdateCountry")]
        public IActionResult UpdateCountry(CountryModel countryModel)
        {
            try
            {
                return Ok(_countryService.UpdateCountry(countryModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete]
        [Route("DeleteCountry")]
        public IActionResult DeleteCountry(int id)
        {
            try
            {
                return Ok(_countryService.DeleteCountry(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
