using Acorna.Core.Entity;
using Acorna.Core.Entity.Notification;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Models.Chat;
using Acorna.Core.Models.Notification;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Models.SystemDefinition;
using Acorna.DTO.Security;
using AutoMapper;

namespace Acorna.DTO
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserList>().ReverseMap();
            CreateMap<User, UserRegister>().ReverseMap();
            CreateMap<User, UserLogin>().ReverseMap();

            //just for test
            CreateMap<Job, JobModel>().ReverseMap();
            CreateMap<GeneralSetting, GeneralSettingModel>().ReverseMap();
            CreateMap<Chat, ChatMessageModel>().ReverseMap();
            CreateMap<Language, LanguageModel>().ReverseMap();
            CreateMap<Group, GroupModel>().ReverseMap();
            CreateMap<Operator, OperatorModel>().ReverseMap();

            CreateMap<NotificationType, NotificationTypeModel>().ReverseMap();

        }
    }
}
