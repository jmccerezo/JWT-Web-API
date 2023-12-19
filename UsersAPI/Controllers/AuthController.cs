using UsersAPI.Dto;
using UsersAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserDto>> SignupUser(UserSignupDto userSignupDto)
        {
            var userExists = _userService.CheckUsername(userSignupDto.Username);

            if (userExists) return BadRequest("Username is already taken");

            var newUser = await _userService.SignupUser(userSignupDto);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> LoginUser(UserLoginDto userLoginDto)
        {
            var user = await _userService.LoginUser(userLoginDto);

            if (user == null) return BadRequest("Incorrect Username or Password");

            return Ok(user);
        }
    }
}
