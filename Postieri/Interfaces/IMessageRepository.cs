using System.Collections.Generic;
using System.Threading.Tasks;
using Postieri.Entities;
using Postieri.Helpers;
using Postieri.DTOs;

namespace Postieri.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<PagedList<MessageDto>> GetMessagesForUser();
        Task<IEnumerable<MessageDto>> GetMessageThread(int currentUserId, int recipientId);
        Task<bool> SaveAllAsync();
    }
}
