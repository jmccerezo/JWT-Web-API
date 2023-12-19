using UsersAPI.Dto;
using UsersAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserDto>> SignupUser(UserSignupDto userSignupDto)
        {
            var userExists = _usersService.CheckUsername(userSignupDto.Username);

            if (userExists) return BadRequest("Username is already taken");

            var newUser = await _usersService.SignupUser(userSignupDto);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> LoginUser(UserLoginDto userLoginDto)
        {
            var user = await _usersService.LoginUser(userLoginDto);

            if (user == null) return BadRequest("Incorrect Username or Password");

            return Ok(user);
        }
    }
}
