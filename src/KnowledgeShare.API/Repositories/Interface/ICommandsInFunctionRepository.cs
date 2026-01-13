using System.ComponentModel.Design;
using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Repositories.Interface
{
    public interface ICommandsInFunctionRepository
    {
        Task<List<Command>> GetCommandsInFunction(string functionId);
        Task<List<Command>> GetCommandsNotInFunction(string functionId);
        Task<CommandInFunction> CreateCommandToFunction(CommandInFunction commandInFunction);
        Task<bool> ExistsAsync(string commandId, string functionId);
        Task<bool> DeleteCommandToFunction(CommandInFunction commandInFunction);
        Task<CommandInFunction> FindCommandInFunction(string commandId, string functionId);
    }
}
