using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;
using Acorna.DTOs.Security;
using Microsoft.AspNetCore.Identity;

public interface ISecurityService
{
    Task<User> GetUserById(int id);
    int GetUsersCountRecord();
    Task<List<UserModel>> GetUsersListAsync();
    Task<List<UserModel>> GetUserBySearchNameAsync(string userName);
    Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);
    Task<List<UserModel>> GetAllSuperAdminsAsync();
    Task<List<UserModel>> GetAllAdminsAsync();
    Task<bool> Delete(int id);
    Task<bool> UpdateUserLanguage(int userId, int languageId);
    Task<LanguageModel> GetLanguageInformations(int userId);
    Task<List<Role>> GetAllRoles();
    Task<bool> UpdateUserRole(UserModel userModel);
    Task<IList<string>> EditRoles(string userName, RoleEdit roleEditDTO);
    Task<User> FindByEmailAsync(string email);
    Task<string> GenerateJwtTokenAsync(User user);
    Task<string> GeneratePasswordResetTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
    Task<IdentityResult> ConfirmEmailAsync(User user, string token);
    Task<object> Login(UserLogin userLogin);
    Task<IdentityResult> CreateUserAsync(UserRegister userRegister);
    Task AddToRoleAsync(UserRegister userRegister, string roleName);
    Task<string> GenerateEmailConfirmationTokenAsync(UserRegister userRegister);
    Task<List<UserModel>> GetUsersByGroupId(int groupId);
    Task<List<UserModel>> GetUsersByCurrentRole(int currentUserId, string currentUserRole);
    Task<bool> IsUserExistsByPhoneNumber(string userPhoneNumber);
}