using KnowledgeShare.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly UserManager<User> _userManager;

        public RegisterRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> RegisterAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}
