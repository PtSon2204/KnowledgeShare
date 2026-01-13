using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.ViewModels;

namespace KnowledgeShare.API.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommanRepository _commanRepository;

        public CommandService(ICommanRepository commanRepository)
        {
            _commanRepository = commanRepository;
        }

        public async Task<List<CommandVm>> GetAllCommandVmAsync()
        {
            var list = await _commanRepository.GetAllCommanAsync();

            return list.Select(x => new CommandVm
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
