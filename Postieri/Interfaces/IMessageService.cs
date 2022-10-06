using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postieri.DTOs;
using Postieri.Helpers;
using Postieri.Models;


namespace Postieri.Interfaces
{
    public interface IMessageService
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<List<MessageDto>> GetMessagesForAgent(MessageParams messageParams);
        Task<IEnumerable<MessageDto>> GetMessageThread(Guid currentConnId, Guid recipientConnId);
        Task<bool> SaveAllAsync();
    }
}
