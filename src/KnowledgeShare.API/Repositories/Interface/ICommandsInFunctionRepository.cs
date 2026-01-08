using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface ICommandsInFunctionRepository
    {
        Task<List<Command>> GetCommandsInFunction(string functionId);
        Task<List<Command>> GetCommandsNotInFunction(string functionId);
        
    }
}
