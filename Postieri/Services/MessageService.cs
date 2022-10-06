using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postieri.Data;
using Postieri.Helpers;
using Postieri.Models;
using Postieri.Interfaces;
using Postieri.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Postieri.Services
{
    public class MessageService : IMessageService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        public MessageService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void AddMessage(Message message)
        {
            _db.Messages.Add(message);
        }
        public void DeleteMessage(Message message)
        {
            _db.Messages.Remove(message);
        }
        public async Task<Message> GetMessage(int id)
        {
            return await _db.Messages.FindAsync(id);
        }

        public async Task<List<MessageDto>> GetMessagesForAgent(MessageParams messageParams)
        {
            var query = _db.Messages.OrderByDescending(m => m.MessageSent).AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Recipient.AgentName == messageParams.Username),
                "Outbox" => query.Where(u => u.Sender.AgentName == messageParams.Username),
                _ => query.Where(u => u.Recipient.AgentName == messageParams.Username && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return (List<MessageDto>)messages;
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(Guid currentConnId, Guid recipientConnId)
        {
            var messages = await _db.Messages.Include(u => u.Sender).Include(u => u.Recipient)
                .Where(m => m.Recipient.ConnectionId == currentConnId
                && m.Sender.ConnectionId == recipientConnId
                || m.Recipient.ConnectionId == recipientConnId
                && m.Sender.ConnectionId == currentConnId
                )
                .OrderBy(m => m.MessageSent).ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null
                && m.Recipient.ConnectionId == currentConnId).ToList();

            if(unreadMessages.Any())
            {
                foreach(var message in unreadMessages)
                {
                    message.DateRead = DateTime.Now;
                }
                await _db.SaveChangesAsync();
            }
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
