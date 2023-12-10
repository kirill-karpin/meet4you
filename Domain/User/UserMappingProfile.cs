using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using User.Dto;

namespace User
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<UserDto, Entities.User>();
            CreateMap<Entities.User, UserDto>();
        }

    }
}
