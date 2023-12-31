using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Sheard;
using Acorna.DTOs.Security;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISecurityRepository
{
    Task<UserModel> GetUserById(int id);
    Task<List<UserModel>> GetUsersListAsync();
    Task<List<UserModel>> GetUserBySearchNameAsync(string userName);
    Task<PaginationRecord<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10, int currentUserId = 0, string currentUserRole = "");
    Task<List<UserModel>> GetUsersByGroupIdAsync(int groupId);
    Task<List<UserModel>> GetAllUsersByTypeAsync(string userType);
    int GetUsersCountRecord();
    Task<bool> Delete(int id);
    Task<bool> UpdateUserLanguage(int userId, int languageId);
    Task<LanguageModel> GetLanguageInformations(int userId);
    Task<List<Role>> GetAllRoles();
    Task<bool> UpdateUserRole(UserRegister userRegister);
    Task<IList<string>> EditRoles(string userName, RoleEdit roleEditDTO);
    Task<User> FindByEmailAsync(string email);
    Task<string> GenerateJwtTokenAsync(User user);
    Task<string> GeneratePasswordResetTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
    Task<IdentityResult> ConfirmEmailAsync(User user, string token);
    Task<object> Login(UserLogin userLogin);
    Task<IdentityResult> AddUserAsync(UserRegister userRegister);
    Task<IdentityResult> UpdateUserAsync(UserRegister userRegister);
    Task<IdentityResult> UpdateUserNameAsync(UserRegister userRegister);
    Task AddToRoleAsync(UserRegister userRegister, string roleName);
    Task<string> GenerateEmailConfirmationTokenAsync(UserRegister userRegister);
    Task<bool> IsUserExistsByPhoneNumber(string phoneNumber);
    Task<int> SearchByPhoneNumber(string phoneNumber);
    Task<int> CreateUserUsingPhoneNumber(string phoneNumber, int simTypeId, int simProfileId);
    Task<User> FindUserByPhoneNumber(string phoneNumber);
    Task<string> GetEmailByUserId(int userId);
    Task<List<string>> GetSuperAdminWithAdminGropByUserId(int userId);
    Task<bool> ChangePassword(int userId, string oldPassword, string newPassword);
    Task<string> GenerateOtpEmailCodeWithUpdateUser(string email);
    Task<VerificationCodeResponseModel> VerifyEmailCode(VerificationCodeModel verificationCodeModel);
    Task<UserModel> GetUserByPhoneNumber(string phoneNumber);
}