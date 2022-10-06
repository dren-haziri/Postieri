using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postieri.DTOs;
using Postieri.Models;
using AutoMapper;

namespace Postieri.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Message, MessageDto>();
            CreateMap<LiveAgent, LiveAgentDto>();
        }
    }
}
