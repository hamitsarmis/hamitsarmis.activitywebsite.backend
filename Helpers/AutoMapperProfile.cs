using AutoMapper;
using hamitsarmis.activitywebsite.backend.DTOs;
using hamitsarmis.activitywebsite.backend.Entities;

namespace hamitsarmis.activitywebsite.backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventDto, Event>();
            CreateMap<UserDto, AppUser>();
        }
    }
}
