using AutoMapper;
using JWTWebAPI.Dto;
using JWTWebAPI.Models;

namespace JWTWebAPI.Profiles
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
