using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using User.Dto;

namespace User.Mapping
{
    public class ResetPasswordProfile : Profile
    {
        public ResetPasswordProfile()
        {
            CreateMap<ResetPassword, ResetPasswordDto>().ReverseMap();

        }

    }
}