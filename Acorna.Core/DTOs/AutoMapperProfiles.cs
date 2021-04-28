using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Entity;
using Acorna.Core.Entity.Notification;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Models.Chat;
using Acorna.Core.Models.Notification;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Models.SystemDefinition;
using Acorna.DTOs.Security;
using AutoMapper;

namespace Acorna.DTOs
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
            CreateMap<Country, CountryModel>().ReverseMap();
            CreateMap<PhoneBook, PhoneBookModel>().ReverseMap();
            CreateMap<Governorate, GovernorateModel>().ReverseMap();
            CreateMap<NotificationType, NotificationTypeModel>().ReverseMap();

            //DTOs
            CreateMap<Governorate, GovernorateDTO>().ReverseMap();
            CreateMap<GovernorateModel, GovernorateDTO>().ReverseMap();
            CreateMap<PhoneBookModel, PhoneBookDTO>().ReverseMap();



        }
    }
}
