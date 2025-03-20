using AuthApi.Application.DTOs;
using AuthApi.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.Application.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<User, UserGetDto>();
            CreateMap<UserLoginDto, User>();
        }
    }
}
