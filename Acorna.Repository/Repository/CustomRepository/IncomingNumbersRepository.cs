using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Project.BillingSystem;
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
    internal class IncomingNumbersRepository : Repository<PhoneBook>, IIncomingNumbersRepository
    {
        private readonly IDbFactory _dbFactory;

        internal IncomingNumbersRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<PaginationRecord<PhoneBookModel>> GetAllIncomingNumbers(int pageIndex, int pageSize, int statusNumber)
        {
            try
            {
                List<PhoneBookModel> phoneBookModel = await (from phone in _dbFactory.DataContext.PhoneBook
                                                             join usr in _dbFactory.DataContext.Users on phone.CreatedBy equals usr.Id
                                                             join grop in _dbFactory.DataContext.Group on usr.GroupId equals grop.Id
                                                             where (phone.PersonalUserId == null || phone.PersonalUserId == 0)
                                                             && phone.StatusNumberId == statusNumber
                                                             select new PhoneBookModel
                                                             {
                                                                 Id = phone.Id,
                                                                 CreatedDate = phone.CreatedDate,
                                                                 PhoneNumber = phone.PhoneNumber,
                                                                 PhoneName = phone.PhoneName,
                                                                 TypePhoneNumberId = phone.TypePhoneNumberId,
                                                                 UserName = usr.UserName,
                                                                 StatusNumberId = phone.StatusNumberId,
                                                                 GroupNameAr = grop.GroupNameAr,
                                                                 GroupNameEn = grop.GroupNameEn

                                                             }).OrderByDescending(s => s.Id).ThenBy(s => s.GroupNameAr)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();


                if (phoneBookModel.Count > 0)
                {
                    phoneBookModel.ForEach(x =>
                    {
                        x.TypePhoneNumberName = Enum.GetName(typeof(TypesPhoneNumber), Convert.ToInt32(x.TypePhoneNumberId));
                        x.StatusNumberName = Enum.GetName(typeof(StatusCycleBills), Convert.ToInt32(x.StatusNumberId));
                    });
                }

                PaginationRecord<PhoneBookModel> paginationRecordModel = new PaginationRecord<PhoneBookModel>
                {
                    DataRecord = phoneBookModel,
                    CountRecord = _dbFactory.DataContext.PhoneBook.Where(x => x.PersonalUserId == null || x.PersonalUserId == 0 && x.StatusNumberId == statusNumber).Count()
                };

                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginationRecord<PhoneBookModel>> GetIncomingNumbersByGroupId(int pageIndex, int pageSize, int statusNumber, int userId)
        {
            try
            {
                User user = await _dbFactory.DataContext.Users.FindAsync(userId);
                int countRecord = 0;

                List<PhoneBookModel> phoneBookModel = await (from phone in _dbFactory.DataContext.PhoneBook
                                                             join usr in _dbFactory.DataContext.Users on phone.CreatedBy equals usr.Id
                                                             join grop in _dbFactory.DataContext.Group on usr.GroupId equals grop.Id
                                                             where (phone.PersonalUserId == null || phone.PersonalUserId == 0)
                                                             && usr.GroupId == user.GroupId
                                                             && phone.StatusNumberId == statusNumber
                                                             select new PhoneBookModel
                                                             {
                                                                 Id = phone.Id,
                                                                 CreatedDate = phone.CreatedDate,
                                                                 PhoneNumber = phone.PhoneNumber,
                                                                 PhoneName = phone.PhoneName,
                                                                 TypePhoneNumberId = phone.TypePhoneNumberId,
                                                                 UserName = usr.UserName,
                                                                 StatusNumberId = phone.StatusNumberId,
                                                                 GroupNameAr = grop.GroupNameAr,
                                                                 GroupNameEn = grop.GroupNameEn

                                                             }).ToListAsync();


                countRecord = phoneBookModel.Count();
                phoneBookModel = phoneBookModel.OrderByDescending(s => s.Id).ThenBy(s => s.GroupNameAr)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToList();
                if (phoneBookModel.Count > 0)
                {
                    phoneBookModel.ForEach(x =>
                    {
                        x.TypePhoneNumberName = Enum.GetName(typeof(TypesPhoneNumber), Convert.ToInt32(x.TypePhoneNumberId));
                        x.StatusNumberName = Enum.GetName(typeof(StatusCycleBills), Convert.ToInt32(x.StatusNumberName));
                    });
                }

                PaginationRecord<PhoneBookModel> paginationRecordModel = new PaginationRecord<PhoneBookModel>
                {
                    DataRecord = phoneBookModel,
                    CountRecord = countRecord
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
