using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Acorna.Repository.DataContext;

public class SecurityRepository : ISecurityRepository
{
    protected readonly AcornaContext _teamDataContext;

    public SecurityRepository(AcornaContext teamDataContext)
    {
        try
        {
            _teamDataContext = teamDataContext;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<object> GetUsersListAsync()
    {
        return await (from user in _teamDataContext.Users
                      orderby user.Id
                      select new
                      {
                          Id = user.Id,
                          userName = user.UserName,
                          Roles = (from userRole in user.UserRoles
                                   join role in _teamDataContext.Roles
                                   on userRole.RoleId equals role.Id
                                   select role.Name).ToList()
                      }).ToListAsync();
    }

    public async Task<object> GetUserBySearchNameAsync(string searchUserName)
    {
        return await (from user in _teamDataContext.Users
                      select new
                      {
                          user.Id,
                          user.UserName,
                      }).Where(x => x.UserName == searchUserName).ToListAsync();
    }

    public async Task<object> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
    {
        return await (from user in _teamDataContext.Users
                      select new
                      {
                          user.Id,
                          user.UserName,
                      }).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
    }
}