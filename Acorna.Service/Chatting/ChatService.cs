using Acorna.Core.DTOs;
using Acorna.Core.DTOs.Chat;
using Acorna.Core.Entity;
using Acorna.Core.IServices.Chat;
using Acorna.Core.Models.Chat;
using Acorna.Core.Models.Notification;
using Acorna.Core.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Service.Chatting
{
    internal class ChatService : IChatService
    {
        private readonly IMapper _imapper;
        private readonly IUnitOfWork _unitOfWork;

        internal ChatService(IUnitOfWork unitOfWork, IMapper imapper)
        {
            _unitOfWork = unitOfWork;
            _imapper = imapper;
        }

        public ChatMessageModel AddNewMassage(ChatMessageModel chatMessageModel)
        {
            try
            {
                Chat chat = _imapper.Map<Chat>(chatMessageModel);
                _unitOfWork.ChatRepository.Insert(chat);
                ChatMessageModel chatMessage = _imapper.Map<ChatMessageModel>(chat);

                _unitOfWork.NotificationRepository.AddNotificationItem(new NotificationItemModel
                {
                    SenderId = chat.CreatedBy,
                    RecipientId = (chat.CreatedBy == 1) ? 2 : 1,
                    MessageText = "New Message was Added",
                    IsRead = false,
                    Deleted = false,
                    NotificationTypeId = (int)SystemEnum.NotificationType.Chatting,
                });
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
                IEnumerable<ChatMessageDTO> chatMessageDTO = await _unitOfWork.ChatRepository.GetAllChattingMassage(senderId, recipientId);
                IEnumerable<ChatMessageModel> chatMessageModels = _imapper.Map<IEnumerable<ChatMessageModel>>(chatMessageDTO);

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
                int countChatMessageModels = await _unitOfWork.ChatRepository.GetUnReadMessages(userId);
                return countChatMessageModels;
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
                Chat chat = await _unitOfWork.ChatRepository.GetAllAsync(messageId);

                if (chat.RecipientId == userId)
                {
                    chat.IsRead = true;
                    chat.DateRead = DateTime.Now;
                }

                return _unitOfWork.ChatRepository.Update(chat);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
