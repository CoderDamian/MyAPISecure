using MyModels;

namespace MyPersistence.Contracts
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(User user);
        
        Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken use);

        Task<UserRefreshToken?> GetSavedRefreshTokens(string username, string refreshtoken);

        Task DeleteUserRefreshTokens(string username, string refreshToken);

        Task<int> SaveCommitAsync();
    }
}
