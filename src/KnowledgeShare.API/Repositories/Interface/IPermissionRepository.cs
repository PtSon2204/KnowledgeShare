using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface IPermissionRepository
    {
        Task<List<PermissionVm>> GetAllCommandViews();
    }
}
