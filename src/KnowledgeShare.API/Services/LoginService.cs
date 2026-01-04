using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KnowledgeShare.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace KnowledgeShare.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager; // để lấy Role

        public LoginService(ILoginRepository loginRepository, IConfiguration configuration, UserManager<User> userManager)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _loginRepository.LoginAsync(email, password);

            if (user == null) return null;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID của Token
                new Claim("UserId", user.Id.ToString())
            };

            //lấy role của user chèn vào token
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // 4. Tạo Key bảo mật từ chuỗi Secret trong appsettings
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // 5. Tạo đối tượng Token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            // 6. Trả về chuỗi Token đã được mã hóa
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
