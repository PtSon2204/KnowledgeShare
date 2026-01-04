using KnowledgeShare.API.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateRoleAsync(RoleVm roleVm);

        Task<RoleVm?> GetById(string id);

        Task<IdentityResult> UpdateRoleAsync(string roleId, RoleVm roleVm);

        Task<IdentityResult> DeleteRoleAsync(string roleId);

        Task<List<RoleVm>> GetAllRolesAsync();

        Task<Pagination<RoleVm>> GetAllRolesAsync(string keyword, int pageIndex, int pageSize);
    }
}
