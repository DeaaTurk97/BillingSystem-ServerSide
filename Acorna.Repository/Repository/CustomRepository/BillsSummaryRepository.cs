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
    public class BillsSummaryRepository : Repository<Bill>, IBillsSummaryRepository
    {
        private readonly IDbFactory _dbFactory;
        private readonly IMapper _mapper;

        public BillsSummaryRepository(IDbFactory dbFactory, IMapper mapper) : base(dbFactory)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BillsSummaryDTO>> GetBillsSummary(int pageIndex, int pageSize, int currentUserId)
        {
            try
            {
                IEnumerable<BillsSummaryDTO> billsSummary = await _dbFactory.DataContext.Bill
                                                                     .Where(x => x.UserId == currentUserId)
                                                                     .ProjectTo<BillsSummaryDTO>(_mapper.ConfigurationProvider)
                                                                     .OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();

                return billsSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
