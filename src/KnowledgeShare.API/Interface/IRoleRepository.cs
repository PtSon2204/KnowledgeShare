using System.Runtime.CompilerServices;
using KnowledgeShare.API.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Interface
{
    public interface IRoleRepository
    {
        Task<IdentityResult> CreateRoleRepoAsync(IdentityRole role);
        Task<IdentityRole> GetById(string id);
        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
        Task<List<IdentityRole>> GetAllRoleAsync();
        Task<Pagination<IdentityRole>> GetAllRolesAsync(string keyword, int pageIndex, int pageSize);
    }
}
