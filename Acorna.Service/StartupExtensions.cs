using Acorna.Core.IServices.Chat;
using Acorna.Core.IServices.Notification;
using Acorna.Core.IServices.Project;
using Acorna.Core.IServices.SystemDefinition;
using Acorna.Core.Services.Project.BillingSystem;
using Acorna.Repository.DependencyInjection;
using Acorna.Service.Chatting;
using Acorna.Service.Notification;
using Acorna.Service.Project;
using Acorna.Service.Project.BillingSystem;
using Acorna.Service.SystemDefinition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acorna.Service.DependencyInjection
{
    public static class StartupExtensions
    {
        public static void AddDependencyInjectionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjectionRepository(configuration);
            services.AddTransient<IGeneralSettingsService, GeneralSettingsService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IOperatorService, OperatorService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IGovernorateService, GovernorateService>();
        }
    }
}