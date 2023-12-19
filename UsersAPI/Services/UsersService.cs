using AutoMapper;
using UsersAPI.Dto;
using UsersAPI.Models;
using UsersAPI.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace UsersAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UsersService(IUsersRepository usersRepository, IMapper mapper, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        public bool CheckUsername(string username)
        {
            var users = _usersRepository.GetAllUsers().Result;
            return users.Any(u => u.Username == username);
        }

        public async Task<UserDto> SignupUser(UserSignupDto userSignupDto)
        {
            CreatePasswordHash(userSignupDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new()
            {
                Role = userSignupDto.Role,
                FirstName = userSignupDto.FirstName,
                LastName = userSignupDto.LastName,
                Username = userSignupDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            await _usersRepository.SignupUser(user);
            var newUser = _mapper.Map<UserDto>(user);

            return newUser;
        }

        public async Task<UserLoginResponseDto?> LoginUser(UserLoginDto userLoginDto)
        {
            var userLogin = await _usersRepository.LoginUser(userLoginDto.Username);

            if (userLogin == null) return null;

            var verified = VerifyPasswordHash(userLoginDto.Password, userLogin.PasswordHash!, userLogin.PasswordSalt!);

            if (!verified) return null;

            var userDto = _mapper.Map<UserLoginResponseDto>(userLogin);
            userDto.Token = CreateToken(userLogin);

            return userDto;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _usersRepository.GetAllUsers();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _usersRepository.GetUserById(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            User user = new()
            {
                FirstName = userUpdateDto.FirstName,
                LastName = userUpdateDto.LastName
            };

            var updatedUser = await _usersRepository.UpdateUser(id, user);

            return _mapper.Map<UserDto>(updatedUser);
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            var user = await _usersRepository.DeleteUser(id);

            return _mapper.Map<UserDto>(user);
        }
    }
}
