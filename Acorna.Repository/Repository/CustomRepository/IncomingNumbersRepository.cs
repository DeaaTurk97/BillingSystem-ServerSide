using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Repository.ICustomRepsitory;
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

        public async Task<List<PhoneBookDTO>> GetAllIncomingNumbers(int pageIndex, int pageSize, int statusNumber)
        {
            try
            {
                List<PhoneBookDTO> phoneBookDTOs = await (from phone in _dbFactory.DataContext.PhoneBook
                                                          join usr in _dbFactory.DataContext.Users on phone.StatusAdminId equals usr.Id
                                                          join grop in _dbFactory.DataContext.Group on usr.GroupId equals grop.Id
                                                          where (phone.PersonalUserId == null || phone.PersonalUserId == 0) 
                                                          && phone.StatusNumberId == statusNumber
                                                          select new PhoneBookDTO
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

        public async Task<List<PhoneBookDTO>> GetIncomingNumbersByGroupId(int pageIndex, int pageSize, int statusNumber, int userId)
        {
            try
            {
                User user = await _dbFactory.DataContext.Users.FindAsync(userId);

                List<PhoneBookDTO> phoneBookDTOs = await (from phone in _dbFactory.DataContext.PhoneBook
                                                          join usr in _dbFactory.DataContext.Users on phone.PersonalUserId equals usr.Id
                                                          join grop in _dbFactory.DataContext.Group on usr.GroupId equals grop.Id
                                                          where phone.PersonalUserId.ToString() != string.Empty 
                                                          && usr.GroupId == user.GroupId
                                                          && phone.StatusNumberId == statusNumber
                                                          select new PhoneBookDTO
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
