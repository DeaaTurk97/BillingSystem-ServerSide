using Acorna.Core.DTOs.Security;
using Acorna.Core.Entity.Security;
using Acorna.Core.Services;
using Acorna.DTOs.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthintecationController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthintecationController(IUnitOfWorkService unitOfWorkService, IConfiguration configuration)
        {
            _unitOfWorkService = unitOfWorkService;
            _configuration = configuration;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("UserLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            try
            {
                var loginResult = await _unitOfWorkService.SecurityService.Login(userLogin);

                if (loginResult != null)
                {
                    return Ok(loginResult);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                User user = await _unitOfWorkService.SecurityService.FindByEmailAsync(email);

                if (user == null)
                {
                    throw new Exception("User Not Exist!");
                }
                IdentityResult result = await _unitOfWorkService.SecurityService.ConfirmEmailAsync(user, token);
                string userLanguage = user.LanguageId == 1 ? "Arabic" : "English";
                string viewPage = string.Format(result.Succeeded ? "../../Views/{0}/EmailConfirmed" : "../../Views/{0}/Error", userLanguage);
                ViewData["WebSiteURL"] = _configuration.GetSection("AllowedClient").Value;
                return View(viewPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            try
            {
                userRegister.IsActive = true;
                userRegister.SecurityStamp = Guid.NewGuid().ToString();// Add this to avoid error when update roles
                IdentityResult result = await _unitOfWorkService.SecurityService.AddUserAsync(userRegister);

                if (result.Succeeded)
                {
                    await _unitOfWorkService.SecurityService.AddToRoleAsync(userRegister, "Employee");
                    return Ok(userRegister);
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("IsUserExists")]
        [AllowAnonymous]
        public async Task<bool> IsUserExists(string userEmail)
        {
            try
            {
                var userManager = await _unitOfWorkService.SecurityService.FindByEmailAsync(userEmail);
                return (userManager != null) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllUsersAsync")]
        public async Task<IActionResult> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(new
            {
                data = await _unitOfWorkService.SecurityService.GetAllUsersAsync(pageNumber, pageSize),
                total = _unitOfWorkService.SecurityService.GetUsersCountRecord()
            });
        }

        [HttpGet]
        [Route("SearchByUserName")]
        public async Task<IActionResult> SearchByUserName(string searchUserName)
        {
            try
            {
                return Ok(await _unitOfWorkService.SecurityService.GetUserBySearchNameAsync(searchUserName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("IsUserExistsByPhoneNumber")]
        public async Task<IActionResult> IsUserExistsByPhoneNumber(string userPhoneNumber)
        {
            try
            {
                return Ok(await _unitOfWorkService.SecurityService.IsUserExistsByPhoneNumber(userPhoneNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(UserResetPassword userResetPassword)
        {
            try
            {
                if (userResetPassword.Password != userResetPassword.ConfirmPassword ||
                    string.IsNullOrEmpty(userResetPassword.Password) ||
                    string.IsNullOrEmpty(userResetPassword.Email) ||
                    string.IsNullOrEmpty(userResetPassword.Token))
                {
                    throw new Exception("Invalid reset password request!");
                }
                User user = await _unitOfWorkService.SecurityService.FindByEmailAsync(userResetPassword.Email);

                if (user == null)
                {
                    throw new Exception("User Not Exist!");
                }

                var decodeToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(userResetPassword.Token));
                IdentityResult result = await _unitOfWorkService.SecurityService.ResetPasswordAsync(user, decodeToken, userResetPassword.Password);

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(UserResetPassword userResetPassword)
        {
            try
            {
                User user = await _unitOfWorkService.SecurityService.FindByEmailAsync(userResetPassword.Email);
                string token = await _unitOfWorkService.SecurityService.GeneratePasswordResetTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                string emailSubject = user.LanguageId == 1 ? "إعادة ضبط كلمة المرور" : "Reset Password";
                string userLanguage = user.LanguageId == 1 ? "Arabic" : "English";
                string template = $"{_configuration.GetSection("AllowedClient").Value}/auth/reset-password/{token}/{user.Email}";

                return Ok(await _unitOfWorkService.EmailService.SendEmailForgotPasswordAsync(user.Email, emailSubject, template));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}
