using UsersAPI.Dto;
using UsersAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<UserDto>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id:int}"), Authorize]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null) return NotFound("User not found");

            return Ok(user);
        }

        [HttpPatch("{id:int}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _userService.UpdateUser(id, userUpdateDto);

            if (user == null) return NotFound("User not found");

            return Ok(user);
        }

        [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            var user = await _userService.DeleteUser(id);

            if (user == null) return NotFound("User not found");

            return Ok(user);
        }
    }
}
