using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Linq;
using API.Models;
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
        private readonly MusicCollectionContext _context;
        private readonly IMapper _mapper;

        public GenresController(MusicCollectionContext musicCollectionContext,
                                IMapper mapper)
        {
            _context = musicCollectionContext;
            _mapper = mapper;
        }

        //GET api/genres      
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            List<Genre> genres = await _context.Genres.ToListAsync();
            if (!genres.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<GenreReadDto>>(genres));
        }

        //GET api/genres/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            Genre genre = null;

            genre = await _context.Genres.FirstOrDefaultAsync(c => c.Id == id);
            if (genre is null)
                return NotFound();

            return Ok(_mapper.Map<GenreReadDto>(genre));
        }
    }
}
