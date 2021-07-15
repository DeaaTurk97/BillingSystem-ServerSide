using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.DTOs.Chat;
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
using System.Globalization;

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
            CreateMap<ServiceType, ServiceTypeModel>().ReverseMap();
            CreateMap<Bill, BillsSummaryDTO>().ForMember(dest => dest.BillMonth, s => s.MapFrom(x => CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(x.BillDate.Month)))
                                              .ForMember(dest => dest.BillYear, s => s.MapFrom(x => x.BillDate.Year))
                                              .ForMember(dest => dest.IsPaid, s => s.MapFrom(x => x.IsPaid))
                                              .ForMember(dest => dest.BillNote, s => s.MapFrom(x => x.Note))
                                              .ForMember(dest => dest.BillStatus, s => s.MapFrom(x => (x.SubmittedByUser == true && x.SubmittedByAdmin == false) ? "Submitted"
                                              : x.SubmittedByUser == false || x.SubmittedByUser == null ? "Not Submitted"
                                              : (x.SubmittedByUser == true && x.SubmittedByAdmin == true) ? "Approved" : ""))
                                              .ReverseMap();

            CreateMap<TypePhoneNumber, TypePhoneNumberModel>().ReverseMap();

            //DTOs
            CreateMap<ChatMessageModel, ChatMessageDTO>().ReverseMap();

            CreateMap<Governorate, GovernorateDTO>().ForMember(dest => dest.CountryNameAr, s => s.MapFrom(y => y.Country.CountryNameAr))
                                                .ForMember(dest => dest.CountryNameEn, s => s.MapFrom(y => y.Country.CountryNameEn)).ReverseMap();

            CreateMap<GovernorateModel, GovernorateDTO>().ReverseMap();
            CreateMap<PhoneBookModel, PhoneBookDTO>().ReverseMap();
            CreateMap<BillsSummaryModel, BillsSummaryDTO>().ReverseMap();



        }
    }
}
