using KnowledgeShare.API.Repositories.Entities;
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

        public async Task<CommandInFunctionVm> CreateCommandToFunction(CommandInFunctionVm commandInFunctionVm)
        {
            var cif = await _commandsInFunctionRepository.ExistsAsync(commandInFunctionVm.CommandId, commandInFunctionVm.FunctionId);

            if (cif)
            {
                throw new Exception($"Function with id {commandInFunctionVm.FunctionId} is existed");
            }

            var function = new CommandInFunction
            {
                CommandId = commandInFunctionVm.CommandId,
                FunctionId = commandInFunctionVm.FunctionId,
            };

            await _commandsInFunctionRepository.CreateCommandToFunction(function);
            return commandInFunctionVm;
        }

        public async Task<bool> DeleteCommandToFunction(string functionId, string commandId)
        {
            var command = await _commandsInFunctionRepository.FindCommandInFunction(commandId, functionId);

            if (command == null)
            {
                throw new Exception($"This is not existed in function");
            }

           await _commandsInFunctionRepository.DeleteCommandToFunction(command);
            return true;
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
