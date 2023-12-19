using UsersAPI.Models;

namespace UsersAPI.Repository
{
    public interface IUsersRepository
    {
        Task SignupUser(User user);
        Task<User?> LoginUser(string username);
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User?> UpdateUser(int id, User user);
        Task<User?> DeleteUser(int id);
    }
}