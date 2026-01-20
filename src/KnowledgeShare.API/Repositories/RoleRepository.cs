using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RoleRepository(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
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

        public async Task<List<Permission>> GetListPermission(string roleId)
        {
            var list = from p in _context.Permissions
                       join c in _context.Commands on p.CommandId equals c.Id
                       where p.RoleId == roleId
                       select new Permission
                       {
                           FunctionId = p.FunctionId,
                           CommandId = p.CommandId,
                           RoleId = p.RoleId,
                       };
            return await list.ToListAsync();
        }

        public async Task<bool> UpdatePermisstionByRoleId(string roleId, List<Permission> permissions)
        {
            // 1. Tìm các quyền hiện tại của Role
            var existingPermissions = _context.Permissions.Where(x => x.RoleId == roleId);

            // 2. Xóa quyền cũ
            _context.Permissions.RemoveRange(existingPermissions);

            // 3. Thêm quyền mới
            await _context.Permissions.AddRangeAsync(permissions);

            // 4. Lưu thay đổi
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IdentityResult> UpdateRoleAsync(IdentityRole role)
        {
           return await _roleManager.UpdateAsync(role);
        }
    }
}
