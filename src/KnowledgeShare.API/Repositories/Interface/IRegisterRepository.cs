using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface IRegisterRepository
    {
        Task<IdentityResult> RegisterAsync(User user, string password);
    }
}
