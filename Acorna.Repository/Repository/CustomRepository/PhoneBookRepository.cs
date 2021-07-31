using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Core.Sheard;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Acorna.Core.DTOs.SystemEnum;

namespace Acorna.Repository.Repository.CustomRepository
{
    public class PhoneBookRepository : Repository<PhoneBook>, IPhoneBookRepository
    {
        private readonly IDbFactory _dbFactory;

        public PhoneBookRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<PaginationRecord<PhoneBookModel>> GetOfficialPhonesBook(int pageIndex, int pageSize)
        {
            try
            {
                List<PhoneBookModel> phoneBookModel = await (from phone in _dbFactory.DataContext.PhoneBook
                                                             join usr in _dbFactory.DataContext.Users on phone.StatusAdminId equals usr.Id
                                                             where (phone.PersonalUserId == null || phone.PersonalUserId == 0) && phone.StatusAdminId != 0
                                                             select new PhoneBookModel
                                                             {
                                                                 Id = phone.Id,
                                                                 CreatedDate = phone.CreatedDate,
                                                                 PhoneNumber = phone.PhoneNumber,
                                                                 PhoneName = phone.PhoneName,
                                                                 TypePhoneNumberId = phone.TypePhoneNumberId,
                                                                 UserName = usr.UserName,
                                                                 StatusNumberId = phone.StatusNumberId

                                                             }).OrderByDescending(s => s.Id)
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
                    CountRecord = _dbFactory.DataContext.PhoneBook.Where(x => x.PersonalUserId == null || x.PersonalUserId == 0 && x.StatusAdminId != 0).Count()
                };

                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginationRecord<PhoneBookModel>> GetPhonesBookByGroupId(int pageIndex, int pageSize, int userId)
        {
            try
            {
                User user = await _dbFactory.DataContext.Users.FindAsync(userId);
                List<PhoneBookModel> phoneBookModels = new List<PhoneBookModel>();
                int countRecord = 0;

                countRecord = await (from phone in _dbFactory.DataContext.PhoneBook
                                     join usr in _dbFactory.DataContext.Users on phone.PersonalUserId equals usr.Id
                                     where (phone.PersonalUserId.ToString() != string.Empty || phone.PersonalUserId != 0)
                                     && usr.GroupId == user.GroupId
                                     select new PhoneBookModel
                                     {
                                         Id = phone.Id,
                                         CreatedDate = phone.CreatedDate,
                                         PhoneNumber = phone.PhoneNumber,
                                         PhoneName = phone.PhoneName,
                                         TypePhoneNumberId = phone.TypePhoneNumberId,
                                         UserName = usr.UserName,
                                         StatusNumberId = phone.StatusNumberId

                                     }).CountAsync();

                phoneBookModels = phoneBookModels.OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToList();

                if (phoneBookModels.Count > 0)
                {
                    phoneBookModels.ForEach(x =>
                    {
                        x.TypePhoneNumberName = Enum.GetName(typeof(TypesPhoneNumber), Convert.ToInt32(x.TypePhoneNumberId));
                        x.StatusNumberName = Enum.GetName(typeof(StatusCycleBills), Convert.ToInt32(x.StatusNumberName));
                    });
                }

                PaginationRecord<PhoneBookModel> paginationRecordModel = new PaginationRecord<PhoneBookModel>
                {
                    DataRecord = phoneBookModels,
                    CountRecord = countRecord,
                };

                return paginationRecordModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaginationRecord<PhoneBookModel>> GetPhonesBookByUserId(int pageIndex, int pageSize, int userId)
        {
            try
            {
                List<PhoneBookModel> phoneBookModel = await (from phone in _dbFactory.DataContext.PhoneBook
                                                             join usr in _dbFactory.DataContext.Users on phone.PersonalUserId equals usr.Id
                                                             where phone.PersonalUserId == userId
                                                             select new PhoneBookModel
                                                             {
                                                                 Id = phone.Id,
                                                                 CreatedDate = phone.CreatedDate,
                                                                 PhoneNumber = phone.PhoneNumber,
                                                                 PhoneName = phone.PhoneName,
                                                                 TypePhoneNumberId = phone.TypePhoneNumberId,
                                                                 UserName = usr.UserName,
                                                                 StatusNumberId = phone.StatusNumberId

                                                             }).OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();

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
                    CountRecord = _dbFactory.DataContext.PhoneBook.Where(x => x.PersonalUserId == userId).Count(),
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
