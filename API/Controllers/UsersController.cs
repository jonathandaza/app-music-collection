using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Repositories;
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

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,
                               IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //GET api/users/search?name=jhon
        //GET api/users/search?name=jhon&genre=male
        //GET api/users/search?name=jhon&genre=male&age=10
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string name, string genre, int? age)
        {
            var result = await _userRepository.SearchAsync(name, genre, age);
            if (!result.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(result));
        }

        //GET api/users      
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {            
            var result = await _userRepository.GetAsync();
            if (!result.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(result));
        }

        //GET api/users/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userRepository.GetAsync(id);
            if (result is null)
                return NotFound();

            return Ok(_mapper.Map<UserReadDto>(result));
        }


        //POST api/users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userCreateDto)
        {
            User user = _mapper.Map<User>(userCreateDto);
            
            var userDto = _mapper.Map<UserReadDto>(await _userRepository.AddAsync(user));

            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        //PUT api/users/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            User user = await _userRepository.GetAsync(id);
            if (user is null)
                return NotFound("User was not found");

            var userNameAlreadyUsed = await _userRepository.GetByNameAsync(userUpdateDto.Name);
            if (userNameAlreadyUsed.Id != user.Id)
            {
                ModelState.AddModelError("name", "user name already in use");
                return BadRequest();
            }

            _mapper.Map(userUpdateDto, user);  
            
            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

        //PATCH api/users/{id}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PartialUpdate(int id, JsonPatchDocument<UserUpdateDto> patchUserDto)
        {
            User user = await _userRepository.GetAsync(id);
            if (user is null)
                return NotFound("User was not found");

            var userToPatch = _mapper.Map<UserUpdateDto>(user);
            patchUserDto.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))            
                return ValidationProblem(ModelState);            

            _mapper.Map(userToPatch, user);

            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

        //DELETE api/users/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await _userRepository.GetAsync(id);
            if (user is null)
                return NotFound("User was not found");

            await _userRepository.DeleteAsync(id);

            return NoContent();
        }
    }   
}
