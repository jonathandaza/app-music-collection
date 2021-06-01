using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Repositories;
using System.Net.Mime;
using System.Linq;
using AutoMapper;
using DTO;

namespace API.Controllers
{
    [Route("api/genres")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenresController(IGenreRepository genreRepository,
                                IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        //GET api/genres      
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var result = await _genreRepository.GetAsync();
            if (!result.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<GenreReadDto>>(result));
        }

        //GET api/genres/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _genreRepository.GetAsync(id);
            if (result is null)
                return NotFound();

            return Ok(_mapper.Map<GenreReadDto>(result));
        }
    }
}
