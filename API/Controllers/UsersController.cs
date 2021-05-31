using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Linq;
using AutoMapper;
using API.Models;
using DTO;

namespace API.Controllers
{
    [Route("api/users")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly MusicCollectionContext _context;
        private readonly IMapper _mapper;

        public UsersController(MusicCollectionContext musicCollectionContext,
                               IMapper mapper)
        {
            _context = musicCollectionContext;
            _mapper = mapper;
        }

        //GET api/users      
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {            
            List<User> users = await _context.Users.ToListAsync();
            if (!users.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        //GET api/users/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            User user = null;

            user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);         
            if (user is null)
                return NotFound();

            return Ok(_mapper.Map<UserReadDto>(user));
        }


        //POST api/users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userCreateDto)
        {
            User user = _mapper.Map<User>(userCreateDto);
            Genre genre = await _context.Genres.FindAsync(userCreateDto.GenreId);
            if (genre is null)
                return BadRequest("Genre is not valid");

            user.SetGenre(genre);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserReadDto>(user);

            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        //PUT api/users/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto userUpdateDto)
        {            
            User user = await _context.Users.FindAsync(id);
            if (user is null)
                return NotFound();

            Genre genre = await _context.Genres.FindAsync(userUpdateDto.GenreId);
            if (genre is null)
                return BadRequest("Genre is not valid");

            _mapper.Map(userUpdateDto, user);

            user.SetGenre(genre);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //PATCH api/users/{id}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PartialUpdate(int id, JsonPatchDocument<UserUpdateDto> patchUserDto)
        {
            User user = await _context.Users.FindAsync(id);
            if (user is null)
                return NotFound();

            var userToPatch = _mapper.Map<UserUpdateDto>(user);
            patchUserDto.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))            
                return ValidationProblem(ModelState);            

            _mapper.Map(userToPatch, user);

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE api/users/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {            
            User user = await _context.Users.FindAsync(id);
            if (user is null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }   
}
