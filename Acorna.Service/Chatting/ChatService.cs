using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acorna.Core.DTO;
using Acorna.Core.Entity;
using Acorna.Core.IServices.Chat;
using Acorna.Core.IServices.Notification;
using Acorna.Core.Models.Chat;
using Acorna.Core.Models.Notification;
using Acorna.Core.Repository;
using Acorna.Repository.DataContext;

namespace Acorna.Service.Chatting
{
    public class ChatService : IChatService
    {
        private readonly IMapper _imapper;
        private readonly IRepository<Chat> _chatRepository;
        private readonly AcornaContext _teamDataContext;
        private readonly INotificationService _notificationService;

        public ChatService(IMapper imapper, IRepository<Chat> chatRepository, AcornaContext teamDataContext, INotificationService notificationService)
        {
            _imapper = imapper;
            _chatRepository = chatRepository;
            _teamDataContext = teamDataContext;
            _notificationService = notificationService;
        }

        public ChatMessageModel AddNewMassage(ChatMessageModel chatMessageModel)
        {
            try
            {
                _chatRepository.BeginTransaction();

                Chat chat = _imapper.Map<Chat>(chatMessageModel);
                _chatRepository.Insert(chat);
                ChatMessageModel chatMessage = _imapper.Map<ChatMessageModel>(chat);

                if (chat.Id != 0)
                {
                    _notificationService.AddNotificationItem(new NotificationItemModel
                    {
                        SenderId = chat.CreatedBy,
                        RecipientId = (chat.CreatedBy == 1) ? 2 : 1,
                        MessageText = "New Message was Added",
                        IsRead = false,
                        Deleted = false,
                        NotificationTypeId = (int)SystemEnum.NotificationType.Chatting,
                    });
                }
                _chatRepository.CommitTransaction();

                return chatMessage;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ChatMessageModel>> GetAllChattingMassage(int senderId, int recipientId)
        {
            try
            {
                IEnumerable<ChatMessageModel> chatMessageModels = await (from chat in _teamDataContext.Chat
                                                                         join userSender in _teamDataContext.Users on chat.CreatedBy equals userSender.Id
                                                                         join userRecipien in _teamDataContext.Users on chat.CreatedBy equals userRecipien.Id
                                                                         where chat.RecipientId == senderId && chat.CreatedBy == recipientId ||
                                                                         chat.RecipientId == recipientId && chat.CreatedBy == senderId
                                                                         select new ChatMessageModel
                                                                         {
                                                                             Id = chat.Id,
                                                                             CreatedBy = chat.CreatedBy,
                                                                             CreatedDate = chat.CreatedDate,
                                                                             UpdatedBy = chat.UpdatedBy,
                                                                             UpdatedDate = chat.UpdatedDate,
                                                                             MessageText = chat.MessageText,
                                                                             IsRead = chat.IsRead,
                                                                             DateRead = chat.DateRead,
                                                                             SenderName = userSender.UserName,
                                                                             SenderPhotoUrl = userSender.PhotoURL,
                                                                             RecipientId = chat.RecipientId,
                                                                             RecipientName = userRecipien.UserName,
                                                                             RecipientPhotoUrl = userRecipien.PhotoURL
                                                                         }
                                                                   ).OrderByDescending(o => o.CreatedDate).ToListAsync();

                return chatMessageModels;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetUnReadMessages(int userId)
        {
            try
            {
                IEnumerable<Chat> chatMessageModels = await _teamDataContext.Chat.Where(m => m.IsRead == false && m.RecipientId == userId).ToListAsync();
                return chatMessageModels.Count();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<bool> MarkMessageAsRead(int userId, int messageId)
        {
            try
            {
                Chat chat = await _chatRepository.GetSingleAsync(messageId);

                if (chat.RecipientId == userId)
                {
                    chat.IsRead = true;
                    chat.DateRead = DateTime.Now;
                }

                return _chatRepository.Update(chat);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
