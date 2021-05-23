using Acorna.Core.Entity.Security;
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;
using Acorna.Core.Repository;
using Acorna.DTOs.Security;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class SecurityService : ISecurityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _imapper;

    internal SecurityService(IUnitOfWork unitOfWork, IMapper imapper)
    {
        try
        {
            _unitOfWork = unitOfWork;
            _imapper = imapper;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<List<UserModel>> GetUsersListAsync()
    {
        return _unitOfWork.SecurityRepository.GetUsersListAsync();
    }

    public Task<List<UserModel>> GetUserBySearchNameAsync(string searchUserName)
    {
        return _unitOfWork.SecurityRepository.GetUserBySearchNameAsync(searchUserName);
    }

    public Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            return _unitOfWork.SecurityRepository.GetAllUsersAsync(pageNumber, pageSize);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<List<UserModel>> GetAllSuperAdminsAsync()
    {
        try
        {
            return GetAllUsersByTypeAsync("admin");
        }
        catch (Exception)
        {
            throw;
        }

    }

    public Task<List<UserModel>> GetAllAdminsAsync()
    {
        try
        {
            return GetAllUsersByTypeAsync("superadmin");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<List<UserModel>> GetAllUsersByTypeAsync(string userType)
    {
        try
        {
            return _unitOfWork.SecurityRepository.GetAllUsersByTypeAsync(userType);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public int GetUsersCountRecord()
    {
        try
        {
            return _unitOfWork.SecurityRepository.GetUsersCountRecord();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Delete(int id)
    {
        try
        {
            _unitOfWork.SecurityRepository.Delete(id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<bool> UpdateUserLanguage(int userId, int languageId)
    {
        try
        {
            return _unitOfWork.SecurityRepository.UpdateUserLanguage(userId, languageId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<LanguageModel> GetLanguageInformations(int userId)
    {
        try
        {
            return _unitOfWork.SecurityRepository.GetLanguageInformations(userId);
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
            return _unitOfWork.SecurityRepository.GetAllRoles();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<bool> UpdateUserRole(UserModel userModel)
    {
        try
        {
            return _unitOfWork.SecurityRepository.UpdateUserRole(userModel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Task<IList<string>> EditRoles(string userName, RoleEdit roleEditDTO)
    {
        return _unitOfWork.SecurityRepository.EditRoles(userName, roleEditDTO);
    }

    public Task<User> FindByEmailAsync(string email)
    {
        return _unitOfWork.SecurityRepository.FindByEmailAsync(email);
    }

    public Task<string> GenerateJwtTokenAsync(User user)
    {
        return _unitOfWork.SecurityRepository.GenerateJwtTokenAsync(user);
    }

    public Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        return _unitOfWork.SecurityRepository.GeneratePasswordResetTokenAsync(user);
    }

    public Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
    {
        return _unitOfWork.SecurityRepository.ResetPasswordAsync(user, token, password);
    }

    public Task<IdentityResult> ConfirmEmailAsync(User user, string token)
    {
        return _unitOfWork.SecurityRepository.ConfirmEmailAsync(user, token);
    }

    public Task<object> Login(UserLogin userLogin)
    {
        return _unitOfWork.SecurityRepository.Login(userLogin);
    }

    public Task<IdentityResult> CreateUserAsync(UserRegister userRegister)
    {
        return _unitOfWork.SecurityRepository.CreateUserAsync(userRegister);
    }

    public Task AddToRoleAsync(UserRegister userRegister, string roleName)
    {
        return _unitOfWork.SecurityRepository.AddToRoleAsync(userRegister, roleName);
    }

    public Task<string> GenerateEmailConfirmationTokenAsync(UserRegister userRegister)
    {
        return _unitOfWork.SecurityRepository.GenerateEmailConfirmationTokenAsync(userRegister);
    }
}
