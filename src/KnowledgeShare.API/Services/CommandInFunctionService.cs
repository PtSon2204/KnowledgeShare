using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Services
{
    public class CommandInFunctionService : ICommandInFunctionService
    {
        private readonly ICommandsInFunctionRepository _commandsInFunctionRepository;

        public CommandInFunctionService(ICommandsInFunctionRepository commandsInFunctionRepository)
        {
            _commandsInFunctionRepository = commandsInFunctionRepository;
        }

        public async Task<List<CommandVm>> GetCommandsInFunction(string functionId)
        {
            var list = await _commandsInFunctionRepository.GetCommandsInFunction(functionId);

            return list.Select(c => new CommandVm
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
        }

        public async Task<List<CommandVm>> GetCommandsNotInFunction(string functionId)
        {
            var list = await _commandsInFunctionRepository.GetCommandsInFunction(functionId);

            return list.Select(c => new CommandVm
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
        }
    }
}
