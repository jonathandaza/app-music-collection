using API.Models;
using AutoMapper;
using API.Dtos;

namespace API.Profiles
{
    public class GenresProfile : Profile
    {
        public GenresProfile()
        {
            CreateMap<Genre, GenreReadDto>();
            CreateMap<GenreReadDto, Genre>();
        }
    }
}
