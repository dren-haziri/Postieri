using System;
using System.Linq;
using Postieri.DTOs;
using Postieri.Entities;
using AutoMapper;

namespace Postieri.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();
        }
    }
}
