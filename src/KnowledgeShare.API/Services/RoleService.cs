using KnowledgeShare.API.Repositories;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IdentityResult> CreateRoleAsync(RoleVm roleVm)
        {
            var role = new IdentityRole()
            {
                Id = roleVm.Id,
                Name = roleVm.Name,
                NormalizedName = roleVm.Name.ToUpper()
            };
            return await _roleRepository.CreateRoleRepoAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleRepository.GetById(roleId);

            if (role == null) return null;

            return await _roleRepository.DeleteRoleAsync(role);
        }

        public async Task<List<RoleVm>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRoleAsync();

            var list = roles.Select(r => new RoleVm()
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return list;
        }

        public async Task<Pagination<RoleVm>> GetAllRolesAsync(string keyword, int pageIndex, int pageSize)
        {
            var result = await _roleRepository.GetAllRolesAsync(keyword, pageIndex, pageSize);

            return new Pagination<RoleVm>
            {
                Items = result.Items.Select(r => new RoleVm()
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList(),
                TotalRecords = result.TotalRecords
            };
        }

        public async Task<RoleVm?> GetById(string id)
        {
            var role = await _roleRepository.GetById(id);
            if (role == null) return null;

            return new RoleVm
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public async Task<IdentityResult> UpdateRoleAsync(string roleId, RoleVm roleVm)
        {
            var role = await _roleRepository.GetById(roleId);

            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Role not found"
                });
            }

            role.Name = roleVm.Name;
            role.NormalizedName = roleVm.Name.ToUpper();

            return await _roleRepository.UpdateRoleAsync(role);
        }

    }
}
