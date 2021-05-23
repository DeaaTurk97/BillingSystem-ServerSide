using Acorna.Controllers.Base;
using Acorna.Core.Models.Chat;
using Acorna.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Acorna.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : TeamControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public ChatController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpPost]
        [Route("AddNewMessage")]
        public IActionResult AddNewMessage(ChatMessageModel chatMessageModel)
        {
            try
            {
                return Ok(_unitOfWorkService.ChatService.AddNewMassage(chatMessageModel));
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
                return Ok(await _unitOfWorkService.ChatService.GetAllChattingMassage(CurrentUserId, recipientId));
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
                return Ok(await _unitOfWorkService.ChatService.GetUnReadMessages(CurrentUserId));
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
                return Ok(await _unitOfWorkService.ChatService.MarkMessageAsRead(CurrentUserId, messageId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
