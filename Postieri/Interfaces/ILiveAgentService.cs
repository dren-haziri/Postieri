using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface ILiveAgentService
    {
        List<LiveAgent> GetLiveAgents();
        bool AddLiveAgent(LiveAgent agent);
        bool LiveAgentExists(LiveAgent agent);
        bool UpdateLiveAgent(LiveAgent agent);
        bool DeleteLiveAgent(Guid id);
        Task<LiveAgent> GetAgentByIdAsync(Guid connId);
    }
}
