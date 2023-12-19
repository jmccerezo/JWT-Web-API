using UsersAPI.Dto;

namespace UsersAPI.Services
{
    public interface IUsersService
    {
        bool CheckUsername(string username);
        Task<UserDto> SignupUser(UserSignupDto userSignupDto);
        Task<UserLoginResponseDto?> LoginUser(UserLoginDto userLoginDto);
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task<UserDto> UpdateUser(int id, UserUpdateDto userUpdateDto);
        Task<UserDto> DeleteUser(int id);
    }
}