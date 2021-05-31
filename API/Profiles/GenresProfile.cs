using API.Models;
using AutoMapper;
using DTO;

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
