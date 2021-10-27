using Acorna.CommonMember;
using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
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
    public class BillsDetailsRepository : Repository<BillDetails>, IBillsDetailsRepository
    {
        private readonly IDbFactory _dbFactory;

        public BillsDetailsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }


        public async Task<PaginationRecord<UnDefinedNumbersDTO>> GetAllUndefinedNumbers(int billId)
        {
            try
            {
                List<UnDefinedNumbersDTO> billsSummary = await (from b in _dbFactory.DataContext.Bill
                                                                join bd in _dbFactory.DataContext.BillDetails on b.Id equals bd.BillId
                                                                join fb in _dbFactory.DataContext.PhoneBook on bd.PhoneNumber equals fb.PhoneNumber
                                                                into leftJoin
                                                                from fb in leftJoin.DefaultIfEmpty() // implement left join
                                                                where b.Id == billId
                                                                && (bd.TypePhoneNumberId == (int)TypesPhoneNumber.Unknown ||
                                                                fb.TypePhoneNumberId == (int)TypesPhoneNumber.Official)
                                                                && (fb.PersonalUserId == 0 || fb.PersonalUserId == null)
                                                                && (fb.StatusNumberId != (int)StatusCycleBills.Approved)
                                                                select new UnDefinedNumbersDTO
                                                                {
                                                                    Id = fb.Id,
                                                                    DialledNumber = !string.IsNullOrEmpty(fb.PhoneNumber)
                                                                                        ? fb.PhoneNumber : bd.PhoneNumber,
                                                                    TypePhoneNumberId = !string.IsNullOrEmpty(Convert.ToString(fb.TypePhoneNumberId))
                                                                                        ? fb.TypePhoneNumberId : bd.TypePhoneNumberId,
                                                                    PhoneName = fb.PhoneName,
                                                                    StatusNumberId = fb.StatusNumberId,
                                                                }).Distinct().ToListAsync();

                //Adding this to return numbers only
                billsSummary = billsSummary.Where(x => SyriaUtilites.IsStringNumber(x.DialledNumber) == true).ToList();


                if (billsSummary.Count > 0)
                {
                    billsSummary.ForEach(x =>
                    {
                        x.StatusNumberName = Enum.GetName(typeof(StatusCycleBills), Convert.ToInt32(x.StatusNumberId));
                    });
                }

                PaginationRecord<UnDefinedNumbersDTO> paginationRecordModel = new PaginationRecord<UnDefinedNumbersDTO>
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

        public async Task<PaginationRecord<ServicesNeedApprovedDTO>> GetServicesNeedApproval(int billId)
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
                                                                    where b.Id == billId
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
                billsSummary = billsSummary.Where(x => SyriaUtilites.IsStringNumber(x.DialledNumber) == true).ToList();


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
    }
}
