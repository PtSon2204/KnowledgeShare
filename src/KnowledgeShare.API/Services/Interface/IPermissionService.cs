using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Services.Interface
{
    public interface IPermissionService
    {
        Task<List<PermissionVm>> GetAllCommandViews();
    }
}
