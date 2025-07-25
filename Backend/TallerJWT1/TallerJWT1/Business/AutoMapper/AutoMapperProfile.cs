using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entity.DTOs;
using Entity.Models;

namespace Business.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Rol, RolDto>().ReverseMap();

            CreateMap<RolUser, RolUserDto>().ReverseMap();
            CreateMap<RolUser,RolUserUpdateDto>().ReverseMap();

        }
    }
}
