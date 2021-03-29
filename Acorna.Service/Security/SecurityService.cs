using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Acorna.Core.Entity.Security;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Repository;
using Acorna.Repository.DataContext;

public class SecurityService : ISecurityService
{
    protected readonly AcornaDbContext _teamDataContext;
    private readonly IMapper _imapper;
    private readonly UserManager<User> _userManager;

    public SecurityService(IMapper imapper, AcornaDbContext teamDataContext, UserManager<User> userManager)
    {
        try
        {
            _teamDataContext = teamDataContext;
            _imapper = imapper;
            _userManager = userManager;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<UserModel>> GetUsersListAsync()
    {
        List<User> users = await _teamDataContext.Users.ToListAsync();

        return users.Select(x => new UserModel
        {
            UserId = x.Id,
            UserName = x.UserName,
        }).ToList();
    }

    public async Task<List<UserModel>> GetUserBySearchNameAsync(string searchUserName)
    {
        List<User> users = await _teamDataContext.Users.Where(a => a.UserName == searchUserName).ToListAsync();

        return users.Select(x => new UserModel
        {
            UserId = x.Id,
            UserName = x.UserName,
        }).ToList();
    }

    public async Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            IEnumerable<User> users = await GetAllAsync(pageNumber, pageSize, x => x.Id, OrderBy.Descending, new Expression<Func<User, object>>[] { x => x.UserRoles });
            List<Role> roles = await _teamDataContext.Roles.ToListAsync();

            return users.Select(x => new UserModel
            {
                UserId = x.Id,
                UserName = x.UserName,
                RoleId = x.UserRoles.Select(x => x.RoleId).FirstOrDefault(),
                RoleName = roles.Find(role => role.Id == x.UserRoles.Select(rId => rId.RoleId).FirstOrDefault())?.Name ?? string.Empty

            }).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<UserModel>> GetAllSuperAdminsAsync()
    {
        try
        {
            return await GetAllUsersByTypeAsync("admin");
        }
        catch (Exception)
        {
            throw;
        }
        
    }

    public async Task<List<UserModel>> GetAllAdminsAsync()
    {
        try
        {
            return await GetAllUsersByTypeAsync("superadmin");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<UserModel>> GetAllUsersByTypeAsync(string userType)
    {
        try
        {
            List<User> users = await _teamDataContext.Users.Where(user =>
            user.UserRoles.Where(ur => ur.Role.Name.ToLower() == userType).Count() > 0).ToListAsync();

            return users.Select(x => new UserModel
            {
                UserId = x.Id,
                UserName = x.UserName,

            }).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public int GetCountRecord()
    {
        try
        {
            int countRecord = _teamDataContext.Users.ToList().Count;
            return countRecord;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<IEnumerable<User>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<User, int>> keySelector, OrderBy orderBy = OrderBy.Ascending, params Expression<Func<User, object>>[] includeProperties)
    {
        try
        {
            return GetAllAsync(pageIndex, pageSize, keySelector, null, orderBy, includeProperties);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<User, int>> keySelector,
           Expression<Func<User, bool>>[] predicate, OrderBy orderBy, params Expression<Func<User, object>>[] includeProperties)
    {
        try
        {
            var entities = FilterQuery(keySelector, predicate, orderBy, includeProperties);
            var total = await entities.CountAsync();// entities.CountAsync() is different than pageSize
            entities = entities.Paginate(pageIndex, pageSize);
            var list = await entities.ToListAsync();
            return list.ToPaginatedList(pageIndex, pageSize, total);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Delete(int id)
    {
        try
        {
            User user = _teamDataContext.Users.FirstOrDefault(s => s.Id == id);
            if (user != null)
                _teamDataContext.Users.Remove(user);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private IQueryable<User> FilterQuery(Expression<Func<User, int>> keySelector, Expression<Func<User, bool>>[] predicate, OrderBy orderBy, Expression<Func<User, object>>[] includeProperties)
    {
        try
        {
            var entities = IncludeProperties(includeProperties);
            entities = (predicate != null) ? PredicateProperties(predicate) : entities;
            entities = (orderBy == OrderBy.Ascending) ? entities.OrderBy(keySelector) : entities.OrderByDescending(keySelector);
            return entities;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private IQueryable<User> PredicateProperties(Expression<Func<User, bool>>[] predicateProperties)
    {
        try
        {
            IQueryable<User> entities = _teamDataContext.Users;
            foreach (var predicate in predicateProperties)
            {
                entities = entities.Where(predicate);
            }
            return entities;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private IQueryable<User> IncludeProperties(params Expression<Func<User, object>>[] includeProperties)
    {
        try
        {
            IQueryable<User> entities = _teamDataContext.Users;
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> UpdateUserLanguage(int userId, int languageId)
    {
        try
        {
            User user = await _teamDataContext.Users.FindAsync(userId);

            if (user != null)
                user.LanguageId = languageId;

            _teamDataContext.Users.Update(user);
            await _teamDataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<LanguageModel> GetLanguageInformations(int userId)
    {
        try
        {
            Language language = await (from user in _teamDataContext.Users
                                       join lang in _teamDataContext.Language on user.LanguageId equals lang.Id
                                       where user.Id == userId
                                       select new Language
                                       {
                                           Id = lang.Id,
                                           LanguageDirection = lang.LanguageDirection,

                                       }).SingleOrDefaultAsync();

            LanguageModel languageModel = _imapper.Map<LanguageModel>(language);

            return languageModel;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<List<Role>> GetAllRoles()
    {
        try
        {
            List<Role> roles = await _teamDataContext.Roles.ToListAsync();

            return roles;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> UpdateUserRole(UserModel userModel)
    {
        try
        {
            User user = await _teamDataContext.Users.FindAsync(userModel.UserId);
            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            if (roles != null)
            {
                var isDeleted = await _userManager.RemoveFromRolesAsync(user, roles.ToArray());

                if (isDeleted.Succeeded)
                {
                    List<Role> roleList = await GetAllRoles();
                    string roleName = roleList.Find(x => x.Id == userModel.RoleId).Name;
                    await this._userManager.AddToRoleAsync(user, roleName);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
