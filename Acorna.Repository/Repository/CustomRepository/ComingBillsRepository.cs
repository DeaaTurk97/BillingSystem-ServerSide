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
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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
                //List<BillsSummaryDTO> comingBillsDTO = await _dbFactory.DataContext.Bill.Include(x => x.User.Group)
                //                                                    .Where(x => x.StatusBillId == statusBill && x.IsPaid != true)
                //                                                    .ProjectTo<BillsSummaryDTO>(_mapper.ConfigurationProvider)
                //                                                    .OrderByDescending(s => s.Id)
                //                                                    .Skip(pageSize * (pageIndex - 1))
                //                                                    .Take(pageSize)
                //                                                    .ToListAsync();

                //PaginationRecord<BillsSummaryDTO> paginationRecordModel = new PaginationRecord<BillsSummaryDTO>
                //{
                //    DataRecord = comingBillsDTO,
                //    CountRecord = _dbFactory.DataContext.Bill.Where(x => x.StatusBillId == (int)SystemEnum.StatusCycleBills.Submit).Count()
                //};

                //return paginationRecordModel;

                Expression<Func<Bill, bool>>[] predicateProperties = new Expression<Func<Bill, bool>>[] { };
                int countBills = 0;

                var bills = _dbFactory.DataContext.Bill.Include(x => x.User).Include(x => x.User.Group).AsQueryable();

                bills = _dbFactory.DataContext.Bill.Include(x => x.BillDetails).AsQueryable();


                foreach (var predicate in predicateProperties)
                {
                    bills = bills.Where(predicate);
                }

                countBills = bills.Count();

                IEnumerable<BillsSummaryDTO> billsSummary = await bills
                                                                 .Where(x => x.StatusBillId == statusBill && x.IsPaid != true)
                                                                 .OrderByDescending(s => s.Id)
                                                                 .Select(x => new BillsSummaryDTO
                                                                 {
                                                                     Id = x.Id,
                                                                     BillMonth = CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(x.BillDate.Month),
                                                                     BillYear = x.BillDate.Year.ToString(),
                                                                     IsPaid = x.IsPaid,
                                                                     BillNote = x.Note,
                                                                     BillStatus = (x.SubmittedByUser == true && x.SubmittedByAdmin == false) ? "Submitted"
                                              : x.SubmittedByUser == false ? "Not Submitted"
                                              : (x.SubmittedByUser == true && x.SubmittedByAdmin == true) ? "Approved" : "",
                                                                     GroupId = x.User.Group.Id,
                                                                     GroupName = x.User.Group.GroupNameEn,
                                                                     userName = !_dbFactory.DataContext.History.Any(e => e.EffectiveDate <= x.BillDate
                                                                                 && (e.ExpiryDate == null || e.ExpiryDate >= x.BillDate)
                                                                                 && e.PhoneNumber == x.User.PhoneNumber) ? x.User.UserName :
                                                                     _dbFactory.DataContext.History
                                                                            .OrderByDescending(e => e.EffectiveDate)
                                                                            .FirstOrDefault(e => e.EffectiveDate <= x.BillDate
                                                                                && (e.ExpiryDate == null || e.ExpiryDate >= x.BillDate)
                                                                                && e.PhoneNumber == x.User.PhoneNumber).UserName,

                                                                     TotalOfficial = x.BillDetails.Where(bd => bd.TypePhoneNumberId == 2).Select(i => i.CallRetailPrice).Sum(),

                                                                     TotalPersonal = x.BillDetails.Where(bd => bd.TypePhoneNumberId == 3).Select(i => i.CallRetailPrice).Sum(),

                                                                     TotalUnknown = x.BillDetails.Where(bd => bd.TypePhoneNumberId == 4).Select(i => i.CallRetailPrice).Sum(),

                                                                 })
                                                                 .Skip(pageSize * (pageIndex - 1))
                                                                 .Take(pageSize)
                                                                 .ToListAsync();

                PaginationRecord<BillsSummaryDTO> paginationRecordModel = new PaginationRecord<BillsSummaryDTO>
                {
                     DataRecord = billsSummary,
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
