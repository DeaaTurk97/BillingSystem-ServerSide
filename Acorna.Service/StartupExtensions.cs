using Acorna.Core.Services;
using Acorna.Repository.DependencyInjection;
using Acorna.Service.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acorna.Service.DependencyInjection
{
    public static class StartupExtensions
    {
        public static void AddDependencyInjectionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjectionRepository(configuration);
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
        }
    }
}