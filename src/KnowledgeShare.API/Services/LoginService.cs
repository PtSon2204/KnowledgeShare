using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace KnowledgeShare.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager; // để lấy Role
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;

        public LoginService(
     ILoginRepository loginRepository,
     IConfiguration configuration,
     UserManager<User> userManager,
     ApplicationDbContext context,
     RoleManager<IdentityRole> roleManager,
     IUserClaimsPrincipalFactory<User> claimsFactory)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _claimsFactory = claimsFactory;
        }


        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _loginRepository.LoginAsync(email, password);
            if (user == null) return null;

            // Lấy claims gốc từ Identity
            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.ToList();

            // Lấy roles
            var roles = await _userManager.GetRolesAsync(user);

            // Query permissions
            var permissionsQuery =
                from p in _context.Permissions
                join c in _context.Commands on p.CommandId equals c.Id
                join f in _context.Functions on p.FunctionId equals f.Id
                join r in _roleManager.Roles on p.RoleId equals r.Id
                where roles.Contains(r.Name)
                select f.Id + "_" + c.Id;

            var permissions = await permissionsQuery.Distinct().ToListAsync();

            // Add extra claims (GIỐNG IdentityProfileService)
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.Add(new Claim("Permissions", JsonConvert.SerializeObject(permissions)));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            // Create JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
