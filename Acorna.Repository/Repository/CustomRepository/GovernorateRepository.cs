using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Repository.ICustomRepsitory;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        internal GovernorateRepository(IDbFactory dbFactory, IMapper mapper) : base(dbFactory)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GovernorateDTO>> GetGovernorates(int pageIndex, int pageSize)
        {
            try
            {
                IEnumerable<GovernorateDTO> governorates = await _dbFactory.DataContext.Governorate
                                                                     .Include(x => x.Country)
                                                                     .ProjectTo<GovernorateDTO>(_mapper.ConfigurationProvider)
                                                                     .OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();

                return governorates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
