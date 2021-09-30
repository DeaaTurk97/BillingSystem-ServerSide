using Acorna.Core.DTOs;
using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Sheard;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Repository.Repository.CustomRepository
{
    internal class ComingBillsRepository : Repository<Bill>, IComingBillsRepository
    {
        private readonly IDbFactory _dbFactory;
        private readonly IMapper _mapper;

        public ComingBillsRepository(IDbFactory dbFactory, IMapper mapper) : base(dbFactory)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<PaginationRecord<BillsSummaryDTO>> GetAllComingBills(int pageIndex, int pageSize, int statusBill)
        {
            try
            {
                List<BillsSummaryDTO> comingBillsDTO = await _dbFactory.DataContext.Bill.Include(x => x.User.Group)
                                                                    .Where(x => x.StatusBillId == statusBill && x.IsPaid != true)
                                                                    .ProjectTo<BillsSummaryDTO>(_mapper.ConfigurationProvider)
                                                                    .OrderByDescending(s => s.Id)
                                                                    .Skip(pageSize * (pageIndex - 1))
                                                                    .Take(pageSize)
                                                                    .ToListAsync();

                PaginationRecord<BillsSummaryDTO> paginationRecordModel = new PaginationRecord<BillsSummaryDTO>
                {
                    DataRecord = comingBillsDTO,
                    CountRecord = _dbFactory.DataContext.Bill.Where(x => x.StatusBillId == (int)SystemEnum.StatusCycleBills.Submit).Count()
                };

                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginationRecord<BillsSummaryDTO>> GetComingBillsByGroupId(int pageIndex, int pageSize, int statusNumber, int userId)
        {
            try
            {
                List<BillsSummaryDTO> comingBillsDTO = await _dbFactory.DataContext.Bill
                                                                   .Where(x => x.StatusBillId == (int)StatusCycleBills.Submit && x.IsPaid != true)
                                                                   .ProjectTo<BillsSummaryDTO>(_mapper.ConfigurationProvider)
                                                                   .OrderByDescending(s => s.Id)
                                                                   .Skip(pageSize * (pageIndex - 1))
                                                                   .Take(pageSize)
                                                                   .ToListAsync();

                PaginationRecord<BillsSummaryDTO> paginationRecordModel = new PaginationRecord<BillsSummaryDTO>
                {
                    DataRecord = comingBillsDTO,
                    CountRecord = _dbFactory.DataContext.Bill.Where(x => x.StatusBillId == (int)SystemEnum.StatusCycleBills.Submit).Count()
                };

                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginationRecord<BillsSummaryDTO>> GetComingBillsFinance(int pageIndex, int pageSize)
        {
            try
            {
                List<BillsSummaryDTO> comingBillsDTO = await _dbFactory.DataContext.Bill.Include(x => x.User.Group)
                                                                    .Where(x => x.StatusBillId == (int)SystemEnum.StatusCycleBills.Approved && x.IsPaid != true)
                                                                    .ProjectTo<BillsSummaryDTO>(_mapper.ConfigurationProvider)
                                                                    .OrderByDescending(s => s.Id)
                                                                    .Skip(pageSize * (pageIndex - 1))
                                                                    .Take(pageSize)
                                                                    .ToListAsync();

                PaginationRecord<BillsSummaryDTO> paginationRecordModel = new PaginationRecord<BillsSummaryDTO>
                {
                    DataRecord = comingBillsDTO,
                    CountRecord = _dbFactory.DataContext.Bill.Where(x => x.StatusBillId == (int)SystemEnum.StatusCycleBills.Approved).Count()
                };

                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
