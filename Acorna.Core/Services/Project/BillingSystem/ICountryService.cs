using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetAllCountries();
        Task<PaginationRecord<CountryModel>> GetCountries(int pageIndex, int pageSize);
        Task<CountryModel> GetCountryId(int countryId);
        int AddCountry(CountryModel countryModel);
        bool UpdateCountry(CountryModel countryModel);
        bool DeleteCountry(int id);
    }
}
