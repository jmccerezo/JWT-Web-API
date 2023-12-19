using UsersAPI.Dto;
using UsersAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<UserDto>> GetAllUsers()
        {
            var users = await _usersService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id:int}"), Authorize]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _usersService.GetUserById(id);

            if (user == null) return NotFound("User not found");

            return Ok(user);
        }

        [HttpPatch("{id:int}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _usersService.UpdateUser(id, userUpdateDto);

            if (user == null) return NotFound("User not found");

            return Ok(user);
        }

        [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            var user = await _usersService.DeleteUser(id);

            if (user == null) return NotFound("User not found");

            return Ok(user);
        }
    }
}
