using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;

public interface ISecurityService
{
    int GetCountRecord();
    Task<List<UserModel>> GetUsersListAsync();
    Task<List<UserModel>> GetUserBySearchNameAsync(string userName);
    Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);
    Task<List<UserModel>> GetAllSuperAdminsAsync();
    Task<List<UserModel>> GetAllAdminsAsync();
    void Delete(int id);
    Task<bool> UpdateUserLanguage(int userId, int languageId);
    Task<LanguageModel> GetLanguageInformations(int userId);
    Task<List<Role>> GetAllRoles();
    Task<bool> UpdateUserRole(UserModel userModel);

}