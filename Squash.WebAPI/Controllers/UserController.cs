using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Squash.WebAPI.Interfaces.Services;
using Squash.WebAPI.Models.DTOs.User;
using Squash.WebAPI.Models;

namespace Squash.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService UserService, IMapper mapper) : ControllerBase
    {
        private readonly IUserService _userService = UserService;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllAsync()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                var usersDTO = _mapper.Map<IEnumerable<UserReadDTO>>(users);
                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetByIdAsync(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                var userDTO = _mapper.Map<UserReadDTO>(user);
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            if (!await _userService.CreateAsync(user)) return NotFound();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateDTO userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                if (!await _userService.UpdateAsync(user)) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (!await _userService.DeleteAsync(id)) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
