using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Acorna.Core.Repository;
using Acorna.Repository.DataContext;
using Acorna.Core.DependencyInjection;
using Acorna.Repository.Repository;

namespace Acorna.Repository.DependencyInjection
{
    public static class StartupExtensions
    {
        public static void AddDependencyInjectionRepository(this IServiceCollection services, IConfiguration configuration)
        {
            IdentityBuilder builder = services.AddDependencyInjectionCore(configuration);
            builder.AddEntityFrameworkStores<AcornaDbContext>();
            services.AddDbContext<AcornaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AppConnectionString")));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IDbFactory), typeof(DbFactory));
        }
    }
}