using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterService(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterVm user)
        {
            string[] str = user.FullName.Split(' ');
            var userCreate = new User
            {
                UserName = user.Email,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = str[0],
                LastName = str[str.Length - 1],
            }; 

            return await _registerRepository.RegisterAsync(userCreate, user.Password);
        }
    }
}
