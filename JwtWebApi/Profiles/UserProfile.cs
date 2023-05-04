using AutoMapper;
using JwtWebApi.Dto;
using JwtWebApi.Models;

namespace JwtWebApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<UserSignupDto, User>();
            CreateMap<User, UserSignupDto>();

            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserLoginDto>();

            CreateMap<UserLoginResponseDto, User>();
            CreateMap<User, UserLoginResponseDto>();

            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}
