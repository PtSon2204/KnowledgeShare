using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Services.Interface
{
    public interface IRegisterService
    {
        Task<IdentityResult> RegisterAsync(RegisterVm user);
    }
}
