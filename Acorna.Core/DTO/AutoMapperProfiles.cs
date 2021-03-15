﻿using AutoMapper;
using System.Linq;
using Acorna.Core.Entity;
using Acorna.Core.Entity.Notification;
using Acorna.Core.Entity.Security;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Models.Chat;
using Acorna.Core.Models.Notification;
using Acorna.Core.Models.Project;
using Acorna.Core.Models.SystemDefinition;
using Acorna.DTO.Security;

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

            CreateMap<NotificationType, NotificationTypeModel>().ReverseMap();

        }
    }
}
