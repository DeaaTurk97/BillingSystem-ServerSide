using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;
using Acorna.DTOs.Security;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISecurityRepository
{
    Task<List<UserModel>> GetUsersListAsync();
    Task<List<UserModel>> GetUserBySearchNameAsync(string userName);
    Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);
    Task<List<UserModel>> GetAllUsersByTypeAsync(string userType);
    int GetUsersCountRecord();
    void Delete(int id);
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
    Task<bool> IsUserExistsByPhoneNumber(string phoneNumber);
    Task<int> SearchByPhoneNumber(string phoneNumber);
    Task<int> CreateUserUsingPhoneNumber(string phoneNumber);
}