using Acorna.CommonMember;
using Acorna.Core.Entity.Security;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Repository;
using Acorna.DTOs.Security;
using Acorna.Repository.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

internal class SecurityRepository : ISecurityRepository
{
    private readonly IDbFactory _dbFactory;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    internal SecurityRepository(IDbFactory dbFactory, IMapper mapper,
                                UserManager<User> userManager, SignInManager<User> signInManager,
                                IConfiguration configuration)
    {
        try
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<UserModel> GetUserById(int id)
    {
        try
        {
            User user =  await _dbFactory.DataContext.Users.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<UserModel>(user);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<List<UserModel>> GetUsersListAsync()
    {
        return (from user in _dbFactory.DataContext.Users
                orderby user.Id
                select new UserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                }).ToListAsync();
    }

    public Task<List<UserModel>> GetUserBySearchNameAsync(string searchUserName)
    {
        return (from user in _dbFactory.DataContext.Users
                select new UserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                }).Where(x => x.UserName == searchUserName).ToListAsync();
    }

    public async Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            IEnumerable<User> users = await GetAllAsync(pageNumber, pageSize, x => x.Id, OrderBy.Descending, new Expression<Func<User, object>>[] { x => x.UserRoles });
            List<Role> roles = await _dbFactory.DataContext.Roles.ToListAsync();

            return users.Select(x => new UserModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                GroupId = x.GroupId,
                LanguageId = x.LanguageId,
                RoleId = x.UserRoles.Select(x => x.RoleId).FirstOrDefault(),
                RoleName = roles.Find(role => role.Id == x.UserRoles.Select(rId => rId.RoleId).FirstOrDefault())?.Name ?? string.Empty

            }).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<List<UserModel>> GetUsersByGroupIdAsync(int groupId)
    {
        try
        {
            return (from user in _dbFactory.DataContext.Users
                    where user.GroupId == groupId
                    orderby user.UserName
                    select new UserModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                    }).ToListAsync();
        }
        catch (Exception)
        {
            throw;
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
            IQueryable<User> entities = _dbFactory.DataContext.Users;
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
            IQueryable<User> entities = _dbFactory.DataContext.Users;
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

    public async Task<List<UserModel>> GetAllUsersByTypeAsync(string userType)
    {
        try
        {
            List<User> users = await _dbFactory.DataContext.Users.Where(user =>
            user.UserRoles.Where(ur => ur.Role.Name.ToLower() == userType).Count() > 0).ToListAsync();

            return users.Select(x => new UserModel
            {
                Id = x.Id,
                UserName = x.UserName,

            }).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public int GetUsersCountRecord()
    {
        return _dbFactory.DataContext.Users.Count();
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            User user = _dbFactory.DataContext.Users.FirstOrDefault(s => s.Id == id);

            if (user == null)
            {
                throw new Exception("User Not Exist!");
            }

            //Delete User
            await _userManager.DeleteAsync(user);

            var rolesForUser = await GetRolesAsync(user);

            if (rolesForUser.Count() > 0)
            {
                foreach (var role in rolesForUser)
                {
                    // item should be the name of the role
                    var result = await _userManager.RemoveFromRoleAsync(user, role);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<bool> UpdateUserLanguage(int userId, int languageId)
    {
        try
        {
            User user = await _dbFactory.DataContext.Users.FindAsync(userId);

            if (user != null)
                user.LanguageId = languageId;

            _dbFactory.DataContext.Users.Update(user);
            await _dbFactory.DataContext.SaveChangesAsync();

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
            Language language = await (from user in _dbFactory.DataContext.Users
                                       join lang in _dbFactory.DataContext.Language on user.LanguageId equals lang.Id
                                       where user.Id == userId
                                       select new Language
                                       {
                                           Id = lang.Id,
                                           LanguageDirection = lang.LanguageDirection,
                                           LanguageCode = lang.LanguageCode
                                       }).SingleOrDefaultAsync();

            LanguageModel languageModel = _mapper.Map<LanguageModel>(language);

            return languageModel;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public Task<List<Role>> GetAllRoles()
    {
        try
        {
            return _dbFactory.DataContext.Roles.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> UpdateUserRole(UserRegister userRegister)
    {
        try
        {
            User user = await _dbFactory.DataContext.Users.FindAsync(userRegister.Id);
            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            if (roles != null)
            {
                var isDeleted = await _userManager.RemoveFromRolesAsync(user, roles.ToArray());

                if (isDeleted.Succeeded)
                {
                    List<Role> roleList = await GetAllRoles();
                    string roleName = roleList.Find(x => x.Id == userRegister.RoleId).Name;
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IList<string>> EditRoles(string userName, RoleEdit roleEditDTO)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = roleEditDTO.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { }; // same ---> selectedRoles = selectedRoles != null ? selectedRoles : new string[] {};
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                throw new Exception("Something goes wrong with adding roles!");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                throw new Exception("Something goes wrong with removing roles!");

            return await _userManager.GetRolesAsync(user);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        try
        {
            return await _userManager.FindByEmailAsync(email);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> GenerateJwtTokenAsync(User user)
    {
        try
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDedcription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDedcription);

            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        try
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
    {
        try
        {
            return _userManager.ResetPasswordAsync(user, token, password);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<IdentityResult> ConfirmEmailAsync(User user, string token)
    {
        try
        {
            return _userManager.ConfirmEmailAsync(user, token);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<object> Login(UserLogin userLogin)
    {
        try
        {
            var userManager = await FindUserByPhoneNumber(userLogin.PhoneNumber);

            if (userManager == null)
            {
                return null;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(userManager, userLogin.Password, false);

            if (!result.Succeeded)
            {
                return null;
            }
            var appUser = _userManager.Users.FirstOrDefault(u => u.PhoneNumber.ToUpper() == userLogin.PhoneNumber.ToUpper());
            var userToReturn = _mapper.Map<UserList>(appUser);
            return new
            {
                token = GenerateJwtTokenAsync(appUser).Result,
                user = userToReturn
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IdentityResult> AddUserAsync(UserRegister userRegister)
    {
        try
        {
            userRegister.IsActive = true;
            userRegister.SecurityStamp = Guid.NewGuid().ToString();

            Role roles = await _dbFactory.DataContext.Roles.FirstOrDefaultAsync(x => x.Id == userRegister.RoleId);
            User userToCreate = _mapper.Map<User>(userRegister);
            IdentityResult result = _userManager.CreateAsync(userToCreate, userRegister.PasswordHash).Result;

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userToCreate, roles.Name);
            }

            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IdentityResult> UpdateUserAsync(UserRegister userRegister)
    {
        try
        {
            Role roles = await _dbFactory.DataContext.Roles.FirstOrDefaultAsync(x => x.Id == userRegister.RoleId);
            User user =  _userManager.FindByIdAsync(Convert.ToString(userRegister.Id)).Result;

            user.UserName = userRegister.UserName;
            user.Email = userRegister.Email;
            user.PhoneNumber = userRegister.PhoneNumber;
            user.GroupId = userRegister.GroupId;
            user.LanguageId = userRegister.LanguageId;
            userRegister.SecurityStamp = user.SecurityStamp;

            IdentityResult result = _userManager.UpdateAsync(user).Result;

            if (result.Succeeded)
            {
                await UpdateUserRole(userRegister);
            }

            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<bool> IsUserExistsByPhoneNumber(string phoneNumber)
    {
        try
        {
            User user = await _dbFactory.DataContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            return (user != null) ? true : false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task AddToRoleAsync(UserRegister userRegister, string roleName)
    {
        try
        {
            User userManager = await _userManager.FindByNameAsync(userRegister.UserName);
            await _userManager.AddToRoleAsync(userManager, roleName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<string> GenerateEmailConfirmationTokenAsync(UserRegister userRegister)
    {
        try
        {
            User userToCreate = _mapper.Map<User>(userRegister);
            return _userManager.GenerateEmailConfirmationTokenAsync(userToCreate);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<int> SearchByPhoneNumber(string phoneNumber)
    {
        try
        {
            User user = await _dbFactory.DataContext.Users.SingleAsync(x => x.PhoneNumber == phoneNumber);
            return user.Id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<int> CreateUserUsingPhoneNumber(string phoneNumber)
    {
        try
        {
            List<GeneralSetting> generalSettings = _dbFactory.DataContext.GeneralSetting.ToList();
            string defaultPassword = Convert.ToString(generalSettings.Find(x => x.SettingName == "DefaultPassword").SettingValue);
            bool isDefaultPassword = Convert.ToBoolean(generalSettings.Find(x => x.SettingName == "IsDefaultPassword").SettingValue);

            User user = new User
            {
                UserName = phoneNumber,
                Email = string.Format(phoneNumber + "{0}", "@unicef.com"),
                EmailConfirmed = true,
                PhoneNumber = phoneNumber,
                PasswordHash = (isDefaultPassword) ? defaultPassword : Utilites.GetRandomPassword(9),
                SecurityStamp = Guid.NewGuid().ToString(),
                IsActive = true,
                LanguageId = 1,
                GroupId = 2,
            };

            var resultCreateUser = await _userManager.CreateAsync(user, user.PasswordHash);

            if (!resultCreateUser.Succeeded)
            {
                throw new Exception("Failed to create local user account.");
            }

            var resultCreateRole = await _userManager.AddToRoleAsync(user, "Employee");

            if (!resultCreateRole.Succeeded)
            {
                throw new Exception("Failed to create Role for user.");
            }

            return user.Id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        try
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<User> FindUserByPhoneNumber(string userPhoneNumber)
    {
        try
        {
            return await _dbFactory.DataContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == userPhoneNumber);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}