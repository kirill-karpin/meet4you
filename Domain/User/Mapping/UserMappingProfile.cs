using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using User.Dto;

namespace User.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            //CreateMap<UserDto, User>()
            //    //.ForAllOtherMembers(x => x.Ignore());
            ////.ForMember(m => m.Email, o => o.MapFrom(s => new String(string.Empty)))
            ////.ForMember(m => m.Password, o => o.MapFrom(s => new String(string.Empty)))
            ////.ForMember(m => m.Confirmed_At, o => o.MapFrom(s => DateTime.MinValue));

            //.ForMember(dest => dest.Password, opt => opt.Ignore())
            //.ForMember(dest => dest.Confirmed_At, opt => opt.Ignore())
            //.ForMember(dest => dest.Email, opt => opt.Ignore());
            ////.ForMember(dest => dest.Email,
            // c => c.MapFrom(new String("")));

            //.ForMember(dto => dto.Email, opt => opt.NullSubstitute(string.Empty));

            //.ForMember(dest => dest.Confirmed_At,
            // c => c.MapFrom(s => DateTime.MinValue))
            //.ForMember(dest => dest.Password,
            // c => c.MapFrom(s => " "));





            CreateMap<User, UserDto_WithLoginPassword>().ReverseMap();

        }

    }
}