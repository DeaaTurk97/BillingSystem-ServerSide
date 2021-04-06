using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Sheard;
using Acorna.Repository.DataContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Acorna.Core.DTOs.billingSystem;

namespace Acorna.Repository.Repository.CustomRepository
{
    public class GovernorateRepository : Repository<Governorate>, IGovernorateRepository
    {
        public GovernorateRepository(AcornaDbContext dbContext): base(dbContext)
        {

        }

        public async Task<IEnumerable<GovernorateDTO>> GetGovernorates(int pageIndex, int pageSize)
        {
            try
            {
                IEnumerable<GovernorateDTO> governorates = await (from gover in _dbContext.Governorate
                                                                    join cou in _dbContext.Country on
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
