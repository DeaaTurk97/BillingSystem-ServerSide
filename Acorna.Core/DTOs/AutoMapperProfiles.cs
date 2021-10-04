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
using Acorna.Core.Models.Security;
using Acorna.Core.Models.SystemDefinition;
using Acorna.DTOs.Security;
using AutoMapper;
using System.Globalization;
using System.Linq;

namespace Acorna.DTOs
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserList>().ReverseMap();
            CreateMap<User, UserRegister>().ReverseMap();
            CreateMap<User, UserLogin>().ReverseMap();
            CreateMap<User, UserModel>().ForMember(dest => dest.RoleId, s => s.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Id))
                                        .ForMember(dest => dest.RoleName, s => s.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Name))
                                        .ForMember(dest => dest.GroupName, s => s.MapFrom(x => x.Group.GroupNameEn))
                                        .ReverseMap();

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
            CreateMap<ServiceUsed, ServiceUsedModel>().ReverseMap();
            CreateMap<Bill, BillsSummaryDTO>().ForMember(dest => dest.BillMonth, s => s.MapFrom(x => CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(x.BillDate.Month)))
                                              .ForMember(dest => dest.BillYear, s => s.MapFrom(x => x.BillDate.Year))
                                              .ForMember(dest => dest.IsPaid, s => s.MapFrom(x => x.IsPaid))
                                              .ForMember(dest => dest.BillNote, s => s.MapFrom(x => x.Note))
                                              .ForMember(dest => dest.BillStatus, s => s.MapFrom(x => (x.SubmittedByUser == true && x.SubmittedByAdmin == false) ? "Submitted"
                                              : x.SubmittedByUser == false || x.SubmittedByUser == null ? "Not Submitted"
                                              : (x.SubmittedByUser == true && x.SubmittedByAdmin == true) ? "Approved" : ""))
                                              .ForMember(dest => dest.userName, s => s.MapFrom(x => x.User.UserName))
                                              .ForMember(dest => dest.GroupId, s => s.MapFrom(x => x.User.Group.Id))
                                              .ForMember(dest => dest.GroupName, s => s.MapFrom(x => x.User.Group.GroupNameEn))
                                              .ReverseMap();

            CreateMap<TypePhoneNumber, TypePhoneNumberModel>().ReverseMap();

            //DTOs
            CreateMap<ChatMessageModel, ChatMessageDTO>().ReverseMap();

            CreateMap<Governorate, GovernorateDTO>().ForMember(dest => dest.CountryNameAr, s => s.MapFrom(y => y.Country.CountryNameAr))
                                                .ForMember(dest => dest.CountryNameEn, s => s.MapFrom(y => y.Country.CountryNameEn)).ReverseMap();

            CreateMap<GovernorateModel, GovernorateDTO>().ReverseMap();
            CreateMap<PhoneBookModel, PhoneBookDTO>().ReverseMap();
            CreateMap<BillsSummaryModel, BillsSummaryDTO>().ReverseMap();

            //Notification
            CreateMap<Notifications, NotificationItemModel>()
                .ForMember(vm => vm.MessageText, m => m.MapFrom(u => u.NotificationsDetails.FirstOrDefault().MessageText))
                .ReverseMap();

            CreateMap<NotificationType, NotificationTypeModel>().ReverseMap();

            //DTOs
            CreateMap<ChatMessageModel, ChatMessageDTO>().ReverseMap();
            CreateMap<AllocatedUsersService, AllocatedUsersServiceModel>().ForMember(dest => dest.UserId, s => s.MapFrom(y => y.User.Id))
                                                                            .ForMember(dest => dest.ServiceId, s => s.MapFrom(y => y.ServiceUsed.Id))
                                                                            .ReverseMap();

            CreateMap<SimCardType, SimCardTypeModel>().ReverseMap();
            CreateMap<SimProfile, SimProfileModel>().ReverseMap();



        }
    }
}
