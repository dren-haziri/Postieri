using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveAgentController : ControllerBase
    {
        private readonly ILiveAgentService _liveAgentService;
        public LiveAgentController(ILiveAgentService liveAgentService)
        {
            _liveAgentService = liveAgentService;
        }

        [HttpGet]
        public ActionResult<List<LiveAgent>> Get()
        {
            return Ok(_liveAgentService.GetLiveAgents());
        }
        [HttpPost]
        public ActionResult<List<LiveAgent>> AddLiveAgent(LiveAgent agent)
        {
            _liveAgentService.AddLiveAgent(agent);
            return Ok(_liveAgentService.GetLiveAgents());
        }
        [HttpPut]
        public ActionResult<List<LiveAgent>> UpdateLiveAgent(LiveAgent agent)
        {
            _liveAgentService.UpdateLiveAgent(agent);
            return Ok(_liveAgentService.GetLiveAgents());
        }
        [HttpDelete]
        public ActionResult<List<LiveAgent>> DeleteLiveAgent(Guid id)
        {
            _liveAgentService.DeleteLiveAgent(id);
            return Ok(_liveAgentService.GetLiveAgents());
        }
    }
}
