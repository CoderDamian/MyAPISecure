using Microsoft.EntityFrameworkCore;
using MyModels;
using MyPersistence.Contracts;

namespace MyPersistence.Repositories
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly MyDbContext _dbContext;

        public UserServiceRepository(MyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _dbContext.UserRefreshTokens.AddAsync(user);

            return user;
        }

        public async Task DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = await _dbContext.UserRefreshTokens.FirstOrDefaultAsync(u => u.UserName == username && u.RefreshToken == refreshToken);

            if (item != null)
            {
                _dbContext.UserRefreshTokens.Remove(item);
            }
        }

        public async Task<UserRefreshToken?> GetSavedRefreshTokens(string username, string refreshtoken)
        {
            return await _dbContext.UserRefreshTokens.FirstOrDefaultAsync(u => u.UserName == username && u.RefreshToken == refreshtoken && u.IsActive == true).ConfigureAwait(false);
        }

        public async Task<bool> IsValidUserAsync(User user)
        {
            User? person = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == user.Name && u.Password == user.Password);

            if (person == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<int> SaveCommitAsync()
        {
            return await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
