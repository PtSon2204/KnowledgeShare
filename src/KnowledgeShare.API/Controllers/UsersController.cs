using KnowledgeShare.API.Authorization;
using KnowledgeShare.API.Constants;
using KnowledgeShare.API.Services;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.API.ViewModels;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.CREATE)]
        public async Task<IActionResult> PostUser([FromBody] UserVm userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.CreateUserAsync(userVm);
            if (result.Succeeded)
            {
                return Ok("Create successfully!");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("{email}")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, CommandCode.VIEW)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //URL: PUT:  http://localhost:7122/api/roles/{id}
        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.UPDATE)]
        public async Task<IActionResult> PutRole(string id, [FromBody] UserVm userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateUserAsync(id, userVm);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{email}")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.DELETE)]
        public async Task<IActionResult> DeteleUser(string email)
        {
            var result = await _userService.DeleteUserAsync(email);

            if (result == null)
                return NotFound();

            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }

        [HttpGet("all")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.VIEW)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{userId}/menu")]
        public async Task<IActionResult> GetMenuByUserPermission(string userId)
        {
            var result = await _userService.GetMenuByUserPermissionAsync(userId);

            return Ok(result);  
        }

        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> PutUserPassword(string id, UserChangePasswordVm userVm)
        {
            var result = await _userService.ChangePasswordAsync(id, userVm);

            if (result.Succeeded)
            {
                return Ok("Change password successfully!");
            }

            return BadRequest(result.Errors);   
        }
    }
}
