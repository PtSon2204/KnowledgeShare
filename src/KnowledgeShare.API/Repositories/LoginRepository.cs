using KnowledgeShare.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded) return user;

            return null;
        }
    }
}
