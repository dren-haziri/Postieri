using Postieri.Data;
using Postieri.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Postieri.Services
{
    public class LiveAgentService : ILiveAgentService
    {
        private readonly DataContext _dataContext;
       

        public LiveAgentService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<LiveAgent> GetLiveAgents()
        {
            return _dataContext.LiveAgents.ToList();
        }
        public bool AddLiveAgent(LiveAgent agent)
        {
            var liveAgent = new LiveAgent()
            {
                AgentId = agent.AgentId,
                AgentName = agent.AgentName,
                IsActive = agent.IsActive,
                ConnectionId = agent.ConnectionId
            };
            if (liveAgent == null)
            {
                return false;
            }
            else if (LiveAgentExists(liveAgent)) return false;
            else
            {
                _dataContext.LiveAgents.Add(liveAgent);
                _dataContext.SaveChanges();
                return true;
            }
        }
        public bool LiveAgentExists(LiveAgent agent)
        {
            bool alreadyExist = _dataContext.LiveAgents.Any(x => x.AgentId == agent.AgentId || x.AgentName == agent.AgentName);
            return alreadyExist;
        }
        public bool UpdateLiveAgent(LiveAgent agent)
        {
            var liveAgent = _dataContext.LiveAgents.Find(agent.AgentId);
            if (liveAgent == null) return false;
            else if (!LiveAgentExists(agent)) return false;
            else
            {
                liveAgent.AgentName = agent.AgentName;
                liveAgent.IsActive = agent.IsActive;
                liveAgent.ConnectionId = agent.ConnectionId;

                _dataContext.SaveChanges();
                return true;
            }
        }
        public bool DeleteLiveAgent(Guid id)
        {
            var agent = _dataContext.LiveAgents.Find(id);
            if (agent == null) return false;
            else if (!LiveAgentExists(agent)) return false;
            else
            {
                _dataContext.LiveAgents.Remove(agent);
                _dataContext.SaveChanges();
                return true;
            }
        }
        public async Task<LiveAgent> GetAgentByIdAsync(Guid connId)
        {
            return await _dataContext.LiveAgents.FindAsync(connId);
        }
    }
}
