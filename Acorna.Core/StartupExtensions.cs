using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Acorna.Core.Entity.Security;

namespace Acorna.Core.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IdentityBuilder AddDependencyInjectionCore(this IServiceCollection services, IConfiguration configuration)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 9;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddDefaultTokenProviders();

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();

            return builder;
        }


    }
}