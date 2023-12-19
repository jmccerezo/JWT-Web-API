using UsersAPI.Data;
using UsersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace UsersAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SignupUser(User user)
        {
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<User?> LoginUser(string username)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> UpdateUser(int id, User user)
        {
            var updateUser = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (updateUser == null) return null;

            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;

            await _dataContext.SaveChangesAsync();

            return updateUser;
        }

        public async Task<User?> DeleteUser(int id)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            _dataContext.Users.Remove(user);

            await _dataContext.SaveChangesAsync();

            return user;
        }
    }
}
