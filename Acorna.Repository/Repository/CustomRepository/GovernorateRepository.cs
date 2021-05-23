using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Repository.ICustomRepsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acorna.Repository.Repository.CustomRepository
{
    internal class GovernorateRepository : Repository<Governorate>, IGovernorateRepository
    {
        private readonly IDbFactory _dbFactory;

        internal GovernorateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IEnumerable<GovernorateDTO>> GetGovernorates(int pageIndex, int pageSize)
        {
            try
            {
                IEnumerable<GovernorateDTO> governorates = await (from gover in _dbFactory.DataContext.Governorate
                                                                    join cou in _dbFactory.DataContext.Country on
                                                                    gover.CountryId equals cou.Id
                                                                    select new GovernorateDTO
                                                                    {
                                                                        Id = gover.Id,
                                                                        GovernorateNameAr = gover.GovernorateNameAr,
                                                                        GovernorateNameEn = gover.GovernorateNameEn,
                                                                        CountryNameAr = cou.CountryNameAr,
                                                                        CountryNameEn = cou.CountryNameEn,
                                                                        CountryId = cou.Id
                                                                    }).OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();

                return governorates;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
