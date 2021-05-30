using AutoMapper;
using API.Models;
using API.Dtos;

namespace API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>()
                .ForMember(p => p.Genre, opt => opt.MapFrom(src => src.Genre));
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }    
}
