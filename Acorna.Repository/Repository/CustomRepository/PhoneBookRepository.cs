using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Repository.ICustomRepsitory;
using Acorna.Repository.DataContext;
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
        public PhoneBookRepository(AcornaDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<PhoneBookDTO>> GetOfficialPhonesBook(int pageIndex, int pageSize)
        {
            try
            {
                List<PhoneBookDTO> phoneBookDTOs = await (from phone in _dbContext.PhoneBook
                                                          join usr in _dbContext.Users on phone.StatusAdminId equals usr.Id
                                                          where (phone.PersonalUserId == null || phone.PersonalUserId == 0)
                                                          select new PhoneBookDTO
                                                          {
                                                              Id = phone.Id,
                                                              PhoneNumber = phone.PhoneNumber,
                                                              PhoneName = phone.PhoneName,
                                                              TypePhoneNumberId = phone.TypePhoneNumberId,
                                                              UserName = usr.UserName,
                                                              StatusNumberId = phone.StatusNumberId

                                                          }).OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();


                if (phoneBookDTOs.Count > 0)
                {
                    phoneBookDTOs.ForEach(x =>
                    {
                        x.TypePhoneNumberName = Enum.GetName(typeof(TypesPhoneNumber), Convert.ToInt32(x.TypePhoneNumberId));
                        x.StatusNumberName = Enum.GetName(typeof(StatusCycleBills), Convert.ToInt32(x.StatusNumberId));
                    });
                }


                return phoneBookDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PhoneBookDTO>> GetPhonesBookByGroupId(int pageIndex, int pageSize, int userId)
        {
            try
            {
                User user = await _dbContext.Users.FindAsync(userId);

                List<PhoneBookDTO> phoneBookDTOs = await (from phone in _dbContext.PhoneBook
                                                          join usr in _dbContext.Users on phone.PersonalUserId equals usr.Id
                                                          where phone.PersonalUserId.ToString() != string.Empty && usr.GroupId == user.GroupId
                                                          select new PhoneBookDTO
                                                          {
                                                              Id = phone.Id,
                                                              PhoneNumber = phone.PhoneNumber,
                                                              PhoneName = phone.PhoneName,
                                                              TypePhoneNumberId = phone.TypePhoneNumberId,
                                                              UserName = usr.UserName,
                                                              StatusNumberId = phone.StatusNumberId

                                                          }).OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();
                if (phoneBookDTOs.Count > 0)
                {
                    phoneBookDTOs.ForEach(x =>
                    {
                        x.TypePhoneNumberName = Enum.GetName(typeof(TypesPhoneNumber), Convert.ToInt32(x.TypePhoneNumberId));
                        x.StatusNumberName = Enum.GetName(typeof(StatusCycleBills), Convert.ToInt32(x.StatusNumberName));
                    });
                }


                return phoneBookDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PhoneBookDTO>> GetPhonesBookByUserId(int pageIndex, int pageSize, int userId)
        {
            try
            {
                List<PhoneBookDTO> phoneBookDTOs = await (from phone in _dbContext.PhoneBook
                                                          join usr in _dbContext.Users on phone.PersonalUserId equals usr.Id
                                                          where phone.PersonalUserId == userId
                                                          select new PhoneBookDTO
                                                          {
                                                              Id = phone.Id,
                                                              PhoneNumber = phone.PhoneNumber,
                                                              PhoneName = phone.PhoneName,
                                                              TypePhoneNumberId = phone.TypePhoneNumberId,
                                                              UserName = usr.UserName,
                                                              StatusNumberId = phone.StatusNumberId

                                                          }).OrderByDescending(s => s.Id)
                                                                     .Skip(pageSize * (pageIndex - 1))
                                                                     .Take(pageSize)
                                                                     .ToListAsync();

                if (phoneBookDTOs.Count > 0)
                {
                    phoneBookDTOs.ForEach(x =>
                    {
                        x.TypePhoneNumberName = Enum.GetName(typeof(TypesPhoneNumber), Convert.ToInt32(x.TypePhoneNumberId));
                        x.StatusNumberName = Enum.GetName(typeof(StatusCycleBills), Convert.ToInt32(x.StatusNumberName));
                    });
                }

                return phoneBookDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
