using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Services.Interface
{
    public interface ICommandService
    {
        Task<List<CommandVm>> GetAllCommandVmAsync();
    }
}
