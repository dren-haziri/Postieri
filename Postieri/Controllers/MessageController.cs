using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postieri.DTOs;
using Postieri.Models;
using Postieri.Helpers;
using Postieri.Interfaces;
using Postieri.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Postieri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILiveAgentService _liveAgentService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(ILiveAgentService liveAgentService, IMessageService messageService,
                                    IMapper mapper)
        {
            _liveAgentService = liveAgentService;
            _messageService = messageService;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            return BadRequest("Failed to send message");
        }

    }
}
