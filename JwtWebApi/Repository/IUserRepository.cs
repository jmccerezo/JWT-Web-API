using JwtWebApi.Dto;
using JwtWebApi.Models;

namespace JwtWebApi.Repository
{
    public interface IUserRepository
    {
        Task SignupUser(User user);
        Task<User> LoginUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(int id, User user);
        Task<List<User>> DeleteUser(int id);
    }
}