using Acorna.CommonMember;
using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Sheard;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acorna.Repository.Repository.CustomRepository
{
    public class BillsDetailsRepository : Repository<BillDetails>, IBillsDetailsRepository
    {
        private readonly IDbFactory _dbFactory;
        private readonly IMapper _mapper;

        public BillsDetailsRepository(IDbFactory dbFactory, IMapper mapper) : base(dbFactory)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }


        public async Task<PaginationRecord<UnDefinedNumbersDTO>> GetAllUndefinedNumbers(int billId)
        {
            try
            {
                IEnumerable<UnDefinedNumbersDTO> billsSummary = await (from b in _dbFactory.DataContext.Bill
                                                                       join bd in _dbFactory.DataContext.BillDetails on b.Id equals bd.BillId
                                                                       where b.Id == billId &&  bd.TypePhoneNumberId == 4 // 4 is Unknown
                                                                       select new UnDefinedNumbersDTO
                                                                       {
                                                                           Id = b.Id,
                                                                           DialledNumber = bd.PhoneNumber,
                                                                           TypePhoneNumberId = bd.TypePhoneNumberId,
                                                                           PhoneName = string.Empty
                                                                       }).Distinct().ToListAsync();
                //Adding this to return numbers only
                billsSummary = billsSummary.Where(x => Utilites.IsStringNumber(x.DialledNumber) == true);

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
    }
}
