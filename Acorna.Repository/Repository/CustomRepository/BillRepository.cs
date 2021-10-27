using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Repository.ICustomRepsitory;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acorna.Repository.Repository.CustomRepository
{
    internal class BillRepository : Repository<Bill>, IBillsRepository
    {
        private readonly IDbFactory _dbFactory;
        private readonly IMapper _mapper;

        internal BillRepository(IDbFactory dbFactory, IMapper mapper) : base(dbFactory)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<List<string>> GetbillsGreaterThanServicesPrices()
        {
            try
            {
                Bill billContainer = new Bill();
                List<Bill> billsLessThanServicePrice = new List<Bill>();
                List<string> usersIDgraterThanServicePrice = new List<string>();

                var allocatedUsersService = await (from aus in _dbFactory.DataContext.AllocatedUsersService
                                                   join u in _dbFactory.DataContext.ServiceUsed on aus.ServiceUsedId equals u.Id
                                                   group u by new { aus.UserId } into gServicesPrices
                                                   select new
                                                   {
                                                       UserId = gServicesPrices.Key.UserId,
                                                       SumServicesPrices = gServicesPrices.Sum(s => s.ServicePrice).Value
                                                   }).ToListAsync();

                var bills = await (from b in _dbFactory.DataContext.Bill
                                   join bd in _dbFactory.DataContext.BillDetails on b.Id equals bd.BillId
                                   where b.SubmittedByUser == false
                                   group bd by new { b.Id, b.UserId } into gSumPrice
                                   select new
                                   {
                                       BillId = gSumPrice.Key.Id,
                                       UserId = gSumPrice.Key.UserId,
                                       SumNetPrice = gSumPrice.Sum(s => s.CallNetPrice),
                                   }).ToListAsync();


                foreach (var bill in bills)
                {
                    var billCheckPrices = allocatedUsersService.Find(x => x.UserId == bill.UserId);

                    if (billCheckPrices != null && bill.SumNetPrice <= billCheckPrices.SumServicesPrices)
                    {
                        billContainer = await _dbFactory.DataContext.Bill.Where(x => x.Id == bill.BillId).SingleAsync();

                        billContainer.SubmittedByUser = true;
                        billContainer.SubmittedByAdmin = true;
                        billContainer.IsPaid = true;
                        billContainer.StatusBillId = 3;

                        billsLessThanServicePrice.Add(billContainer);
                    }
                    else if (billCheckPrices != null && bill.SumNetPrice > billCheckPrices.SumServicesPrices)
                    {
                        usersIDgraterThanServicePrice.Add(Convert.ToString(bill.UserId));
                    }
                }

                _dbFactory.DataContext.UpdateRange(billsLessThanServicePrice);
                await _dbFactory.DataContext.SaveChangesAsync();

                return usersIDgraterThanServicePrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
