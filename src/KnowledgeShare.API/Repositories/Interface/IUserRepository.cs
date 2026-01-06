using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<IdentityResult> DeleteUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
    }
}
