using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acorna.Controllers.Base
{
    [Controller]
    public class TeamControllerBase : ControllerBase
    {
        public TeamControllerBase()
        {

        }

        protected int CurrentUserId => HttpContext.User != null ? int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value) : 0;
        protected string CurrentUserRole => HttpContext.User != null ? HttpContext.User.FindFirst(ClaimTypes.Role).Value : string.Empty;
    }
}
