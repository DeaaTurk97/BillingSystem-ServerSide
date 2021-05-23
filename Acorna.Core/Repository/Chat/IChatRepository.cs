using Acorna.Core.DTOs.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.Chat
{
    public interface IChatRepository : IRepository<Acorna.Core.Entity.Chat>
    {
        Task<IEnumerable<ChatMessageDTO>> GetAllChattingMassage(int senderId, int recipientId);
        Task<int> GetUnReadMessages(int userId);
    }
}
