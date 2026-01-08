using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Services.Interface
{
    public interface ICommandInFunctionService
    {
        Task<List<CommandVm>> GetCommandsInFunction(string functionId);
        Task<List<CommandVm>> GetCommandsNotInFunction(string functionId);
    }
}
