using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Linq;
using AutoMapper;
using API.Models;
using API.Dtos;

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
        public IActionResult Get([FromQuery] int count)
        {            
            List<User> users = _context.Users.Take(count).ToList();
            if (!users.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        //GET api/users/{id}
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            User user = null;

            user = _context.Users.FirstOrDefault(c => c.Id == id);
            //if (user is null)
            //    throw new System.Exception("not found!");            
            if (user is null)
                return NotFound();

            return Ok(_mapper.Map<UserReadDto>(user));
        }


        //POST api/users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] UserCreateDto userCreateDto)
        {
            User user = _mapper.Map<User>(userCreateDto);
            _context.Users.Add(user);
            _context.SaveChanges();

            var userDto = _mapper.Map<UserReadDto>(user);

            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        //PUT api/users/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id,[FromBody] UserUpdateDto userUpdateDto)
        {            
            User user = _context.Users.Find(id);
            if (user is null)
                return NotFound();

            _mapper.Map(userUpdateDto, user);

            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        //PATCH api/users/{id}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PartialUpdate(int id, JsonPatchDocument<UserUpdateDto> patchUserDto)
        {
            User user = _context.Users.Find(id);
            if (user is null)
                return NotFound();

            var userToPatch = _mapper.Map<UserUpdateDto>(user);
            patchUserDto.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))            
                return ValidationProblem(ModelState);            

            _mapper.Map(userToPatch, user);

            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        //DELETE api/users/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {            
            User user = _context.Users.Find(id);
            if (user is null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            
            return NoContent();
        }
    }   
}
