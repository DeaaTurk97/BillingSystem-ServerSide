using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Acorna.Core.Entity.Security;
using Acorna.DTO.Security;

namespace Acorna.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthintecationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        private readonly ISecurityService _securityService;

        public AuthintecationController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager,
                                        IMapper mapper, ISecurityService securityService)
        {
            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _securityService = securityService;
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
                var userManager = await _userManager.FindByEmailAsync(userLogin.Email);

                if (userManager != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(userManager, userLogin.Password, false);

                    if (result.Succeeded)
                    {
                        var appUser = _userManager.Users.FirstOrDefault(u => u.NormalizedEmail.ToUpper() == userLogin.Email.ToUpper());
                        var userToReturn = _mapper.Map<UserList>(appUser);
                        return Ok(new
                        {
                            token = GenerateJwtToken(appUser).Result,
                            user = userToReturn
                        });
                    }
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            try
            {
                var userToCreate = _mapper.Map<User>(userRegister);
                userToCreate.SecurityStamp = Guid.NewGuid().ToString();// Add this to avoid error when update roles
                var result = await _userManager.CreateAsync(userToCreate, userRegister.PasswordHash);
                var userToReturn = _mapper.Map<UserRegister>(userToCreate);
                var userManager = await _userManager.FindByNameAsync(userToCreate.UserName);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userManager, "guest");
                    return Ok(userToReturn);
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        private async Task<string> GenerateJwtToken(User user)
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

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
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

        [HttpGet]
        [Route("IsUserExists")]
        [AllowAnonymous]
        public async Task<bool> IsUserExists(string userEmail)
        {
            try
            {
                var userManager = await _userManager.FindByEmailAsync(userEmail);
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
            return Ok(new { data = await _securityService.GetAllUsersAsync(pageNumber, pageSize), total = _userManager.Users.Count() });
        }

        [HttpGet]
        [Route("SearchByUserName")]
        public async Task<IActionResult> SearchByUserName(string searchUserName)
        {
            try
            {
                return Ok(await _securityService.GetUserBySearchNameAsync(searchUserName));
            }
            catch (Exception)
            {
                return BadRequest("false");
            }
        }
    }
}
