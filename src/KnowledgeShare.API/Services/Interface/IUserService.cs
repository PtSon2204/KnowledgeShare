using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Services.Interface
{
    public interface IUserService
    {
        Task<UserVm> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(UserVm user);
        Task<IdentityResult> UpdateUserAsync(string id, UserVm user);
        Task<IdentityResult> DeleteUserAsync(string email);
        Task<List<UserVm>> GetAllUsersAsync();
        Task<List<FunctionVm>> GetMenuByUserPermissionAsync(string userId);
    }
}
