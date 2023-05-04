using JwtWebApi.Data;
using JwtWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApi.Repository
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

        public async Task<User> LoginUser(User user)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == user.Username);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            var updateUser = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;

            await _dataContext.SaveChangesAsync();

            return updateUser;
        }

        public async Task<List<User>> DeleteUser(int id)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
            return await _dataContext.Users.ToListAsync();
        }
    }
}
