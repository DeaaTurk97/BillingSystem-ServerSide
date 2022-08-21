using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Sheard;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
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
    public class BillsSummaryRepository : Repository<Bill>, IBillsSummaryRepository
    {
        private readonly IDbFactory _dbFactory;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public BillsSummaryRepository(IDbFactory dbFactory, IMapper mapper, UserManager<User> userManager) : base(dbFactory)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PaginationRecord<BillsSummaryDTO>> GetBillsSummary(int pageIndex, int pageSize, int currentUserId, string currentUserRole)
        {
            try
            {
                RolesType rolesType = (RolesType)Enum.Parse(typeof(RolesType), currentUserRole);
                Expression<Func<Bill, bool>>[] predicateProperties = new Expression<Func<Bill, bool>>[] { };
                int countBills = 0;

                var bills = _dbFactory.DataContext.Bill.Include(x => x.User).Include(x => x.User.Group).AsQueryable();

                if (rolesType == RolesType.AdminGroup)
                {
                    User userGroup = _userManager.FindByIdAsync(Convert.ToString(currentUserId)).Result;
                    predicateProperties = new Expression<Func<Bill, bool>>[] { x => x.User.GroupId == userGroup.GroupId };
                }
                else if (rolesType == RolesType.Employee)
                {
                    var userName = _userManager.FindByIdAsync(Convert.ToString(currentUserId)).Result.UserName;
                   

                    var history = _dbFactory.DataContext.History.Where(e => e.UserName == userName).ToList();
                    predicateProperties = new Expression<Func<Bill, bool>>[] {
                        x => x.UserId == currentUserId
                       && (!history.Any() || x.BillDate <= history.Last().EffectiveDate)
                    };
                }


                foreach (var predicate in predicateProperties)
                {
                    bills = bills.Where(predicate);
                }

                countBills = bills.Count();

                IEnumerable<BillsSummaryDTO> billsSummary = await bills
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
                                                                     userName =!_dbFactory.DataContext.History.Any(e=> e.EffectiveDate <= x.BillDate
                                                                                && (e.ExpiryDate == null || e.ExpiryDate >= x.BillDate)
                                                                                && e.PhoneNumber == x.User.PhoneNumber) ? x.User.UserName : 
                                                                     _dbFactory.DataContext.History
                                                                            .OrderByDescending(e=>e.EffectiveDate)
                                                                            .FirstOrDefault(e => e.EffectiveDate <= x.BillDate 
                                                                                && (e.ExpiryDate == null || e.ExpiryDate >= x.BillDate) 
                                                                                && e.PhoneNumber == x.User.PhoneNumber).UserName,


                                                                 })
                                                                 .Skip(pageSize * (pageIndex - 1))
                                                                 .Take(pageSize)
                                                                 .ToListAsync();

                PaginationRecord<BillsSummaryDTO> paginationRecordModel = new PaginationRecord<BillsSummaryDTO>
                {
                    DataRecord = billsSummary,
                    CountRecord = countBills
                };

                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BillsSummaryDTO> GetBillSummaryById(int billId)
        {
            try
            {
                BillsSummaryDTO billsSummary = await _dbFactory.DataContext.Bill
                                                                     .Where(x => x.Id == billId)
                                                                     .ProjectTo<BillsSummaryDTO>(_mapper.ConfigurationProvider)
                                                                     .OrderByDescending(s => s.Id).FirstOrDefaultAsync();

                return billsSummary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BillsSummaryDTO> GetBillsSummaryAmounts(int billId)
        {
 
                BillsSummaryDTO billsSummary = await _dbFactory.DataContext.Bill
                                                                     .Where(x => x.Id == billId)
                                                                     .ProjectTo<BillsSummaryDTO>(_mapper.ConfigurationProvider)
                                                                     .OrderByDescending(s => s.Id).FirstOrDefaultAsync();

                return billsSummary;
        }
    }
}
