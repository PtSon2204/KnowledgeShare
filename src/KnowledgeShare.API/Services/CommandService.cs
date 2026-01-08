using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandService _commandService;

        public CommandService(ICommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<List<CommandVm>> GetAllCommandVmAsync()
        {
            var list = await _commandService.GetAllCommandVmAsync();

            return list.Select(x => new CommandVm
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
