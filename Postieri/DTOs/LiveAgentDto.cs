using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.DTOs
{
    public class LiveAgentDto
    {
        public Guid AgentId { get; set; }
        public string AgentName { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid? ConnectionId { get; set; }
    }
}
