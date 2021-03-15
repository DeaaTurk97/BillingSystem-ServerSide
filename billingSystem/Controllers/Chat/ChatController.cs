using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Acorna.Controllers.Base;
using Acorna.Core.IServices.Chat;
using Acorna.Core.Models.Chat;

namespace Acorna.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : TeamControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        [Route("AddNewMessage")]
        public IActionResult AddNewMessage(ChatMessageModel chatMessageModel)
        {
            try
            {
                return Ok(_chatService.AddNewMassage(chatMessageModel));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllChattingMassage")]
        public async Task<IActionResult> GetAllChattingMassage(int recipientId)
        {
            try
            {
                return Ok(await _chatService.GetAllChattingMassage(CurrentUserId, recipientId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUnReadMessages")]
        public async Task<IActionResult> GetUnReadMessages()
        {
            try
            {
                return Ok(await _chatService.GetUnReadMessages(CurrentUserId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("MarkMessageAsRead")]
        public async Task<IActionResult> MarkMessageAsRead(int messageId)
        {
            try
            {
                return Ok(await _chatService.MarkMessageAsRead(CurrentUserId, messageId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
