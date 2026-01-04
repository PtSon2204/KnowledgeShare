using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRoleRepoAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(IdentityRole role)
        {
            return await _roleManager.DeleteAsync(role);
        }

        public async Task<List<IdentityRole>> GetAllRoleAsync()
        {
            return await _roleManager.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<Pagination<IdentityRole>> GetAllRolesAsync(string keyword, int pageIndex, int pageSize)
        {
            var query = _roleManager.Roles;

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Id.Contains(keyword) || x.Name.Contains(keyword));
            }
            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new Pagination<IdentityRole>
            {
                Items = items,
                TotalRecords = totalRecords
            };
        }

        public async Task<IdentityRole> GetById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> UpdateRoleAsync(IdentityRole role)
        {
           return await _roleManager.UpdateAsync(role);
        }
    }
}
