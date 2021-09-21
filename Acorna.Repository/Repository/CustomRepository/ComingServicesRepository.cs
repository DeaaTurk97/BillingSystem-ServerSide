using Acorna.CommonMember;
using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Sheard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Repository.Repository.CustomRepository
{
    internal class ComingServicesRepository : Repository<BillDetails>, IComingServicesRepository
    {
        private readonly IDbFactory _dbFactory;

        internal ComingServicesRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<PaginationRecord<ServicesNeedApprovedDTO>> GetAllComingServices(int pageIndex, int pageSize, int statusService)
        {
            try
            {
                List<ServicesNeedApprovedDTO> billsSummary = await (from b in _dbFactory.DataContext.Bill
                                                                    join bd in _dbFactory.DataContext.BillDetails on b.Id equals bd.BillId
                                                                    join fb in _dbFactory.DataContext.PhoneBook on bd.PhoneNumber equals fb.PhoneNumber
                                                                    into leftJoin
                                                                    from fb in leftJoin.DefaultIfEmpty() // implement left join
                                                                    join tphn1 in _dbFactory.DataContext.TypePhoneNumber on bd.TypePhoneNumberId equals tphn1.Id
                                                                    join tphn2 in _dbFactory.DataContext.TypePhoneNumber on bd.TypeServiceUsedId equals tphn2.Id
                                                                    join su in _dbFactory.DataContext.ServiceUsed on bd.ServiceUsedId equals su.Id
                                                                    where (bd.IsServiceUsedNeedApproved == true)
                                                                    && (bd.StatusServiceUsedId == statusService)
                                                                    select new ServicesNeedApprovedDTO
                                                                    {
                                                                        Id = bd.Id,
                                                                        DialledNumber = bd.PhoneNumber,
                                                                        TypePhoneNumberId = bd.TypePhoneNumberId,
                                                                        TypePhoneNumberName = tphn1.TypeNameEn,
                                                                        PhoneName = fb.PhoneName,
                                                                        StatusNumberId = bd.StatusServiceUsedId,
                                                                        ServiceUsedName = su.ServiceUsedNameEn,
                                                                        TypeServiceUsedId = bd.TypeServiceUsedId,
                                                                        TypeServiceUsedName = tphn2.TypeNameEn,
                                                                        CallDateTime = bd.CallDateTime,
                                                                        CallDuration = bd.CallDuration,
                                                                        CallRetailPrice = bd.CallRetailPrice
                                                                    }).OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();

                //Adding this to return numbers only
                billsSummary = billsSummary.Where(x => Utilites.IsStringNumber(x.DialledNumber) == true).ToList();


                if (billsSummary.Count > 0)
                {
                    billsSummary.ForEach(x =>
                    {
                        x.StatusNumberName = Enum.GetName(typeof(StatusCycleBills), Convert.ToInt32(x.StatusNumberId));
                    });
                }

                PaginationRecord<ServicesNeedApprovedDTO> paginationRecordModel = new PaginationRecord<ServicesNeedApprovedDTO>
                {
                    DataRecord = billsSummary,
                    CountRecord = billsSummary.Count(),
                };

                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginationRecord<ServicesNeedApprovedDTO>> GetComingServicesByGroupId(int pageIndex, int pageSize, int statusNumber, int userId)
        {
            try
            {
                User user = await _dbFactory.DataContext.Users.FindAsync(userId);

                List<ServicesNeedApprovedDTO> billsSummary = await (from b in _dbFactory.DataContext.Bill
                                                                    join bd in _dbFactory.DataContext.BillDetails on b.Id equals bd.BillId
                                                                    join fb in _dbFactory.DataContext.PhoneBook on bd.PhoneNumber equals fb.PhoneNumber
                                                                    into leftJoin
                                                                    from fb in leftJoin.DefaultIfEmpty() // implement left join
                                                                    join tphn1 in _dbFactory.DataContext.TypePhoneNumber on bd.TypePhoneNumberId equals tphn1.Id
                                                                    join tphn2 in _dbFactory.DataContext.TypePhoneNumber on bd.TypeServiceUsedId equals tphn2.Id
                                                                    join su in _dbFactory.DataContext.ServiceUsed on bd.ServiceUsedId equals su.Id
                                                                    join usr in _dbFactory.DataContext.Users on b.UserId equals usr.Id
                                                                    join grop in _dbFactory.DataContext.Group on usr.GroupId equals grop.Id
                                                                    where usr.GroupId == user.GroupId
                                                                    && (bd.IsServiceUsedNeedApproved == true)
                                                                    && (bd.StatusServiceUsedId != (int)StatusCycleBills.Approved)
                                                                    && b.StatusBillId != (int)StatusCycleBills.Approved
                                                                    select new ServicesNeedApprovedDTO
                                                                    {
                                                                        Id = bd.Id,
                                                                        DialledNumber = bd.PhoneNumber,
                                                                        TypePhoneNumberId = bd.TypePhoneNumberId,
                                                                        TypePhoneNumberName = tphn1.TypeNameEn,
                                                                        PhoneName = fb.PhoneName,
                                                                        StatusNumberId = bd.StatusServiceUsedId,
                                                                        ServiceUsedName = su.ServiceUsedNameEn,
                                                                        TypeServiceUsedId = bd.TypeServiceUsedId,
                                                                        TypeServiceUsedName = tphn2.TypeNameEn,
                                                                        CallDateTime = bd.CallDateTime,
                                                                        CallDuration = bd.CallDuration,
                                                                        CallRetailPrice = bd.CallRetailPrice
                                                                    }).ToListAsync();

                //Adding this to return numbers only
                billsSummary = billsSummary.Where(x => Utilites.IsStringNumber(x.DialledNumber) == true).ToList();


                if (billsSummary.Count > 0)
                {
                    billsSummary.ForEach(x =>
                    {
                        x.StatusNumberName = Enum.GetName(typeof(StatusCycleBills), Convert.ToInt32(x.StatusNumberId));
                    });
                }

                PaginationRecord<ServicesNeedApprovedDTO> paginationRecordModel = new PaginationRecord<ServicesNeedApprovedDTO>
                {
                    DataRecord = billsSummary,
                    CountRecord = billsSummary.Count(),
                };

                return paginationRecordModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
