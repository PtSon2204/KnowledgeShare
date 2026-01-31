using KnowledgeShare.API.Authorization;
using KnowledgeShare.API.Constants;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.API.ViewModels;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        //URL: POST: http://localhost:7122/api/roles
        [HttpPost]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.CREATE)]
        public async Task<IActionResult> PostRole([FromBody] RoleVm roleVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roleService.CreateRoleAsync(roleVm);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = roleVm.Id }, roleVm);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //URL: GET: http://localhost:7122/api/roles/{id}
        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await _roleService.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

           return Ok(role);
        }

        //URL: PUT:  http://localhost:7122/api/roles/{id}
        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.UPDATE)]
        public async Task<IActionResult> PutRole(string id, [FromBody] RoleVm roleVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roleService.UpdateRoleAsync(id, roleVm);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //URL: PUT:  http://localhost:7122/api/roles/{id}
        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.DELETE)]
        public async Task<IActionResult> DeteleRole(string id)
        {
            var result = await _roleService.DeleteRoleAsync(id);

            if (result == null)
                return NotFound();

            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }

        //URL: PUT:  http://localhost:7122/api/roles/
        [HttpGet("all")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        //URL: PUT:  http://localhost:7122/api/roles/?filter={keyword}&pageIndex=1&pageSize=10
        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<IActionResult> GetRoles(string? filter, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _roleService.GetAllRolesAsync(filter, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpGet("{roleId}/permissions")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.VIEW)]
        public async Task<IActionResult> GetPermissionByRoleId(string roleId)
        {
            var permissions = await _roleService.GetPermissionRoleVm(roleId);

            return Ok(permissions);
        }

        [HttpPut("{roleId}/permissions")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.UPDATE)]
        public async Task<IActionResult> PutPermissionByRoleId(string roleId, [FromBody] List<PermissionRoleVm> permissions)
        {
            var result = await _roleService.UpdatePermissionRoleVmAsync(roleId, permissions);

            if (result)
            {
                return NoContent();
            }

            return BadRequest(); 
        }
    }
}
