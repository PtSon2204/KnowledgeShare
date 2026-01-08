using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.Repositories.Interface;

namespace KnowledgeShare.API.Repositories
{
    public class CommandRepository : ICommanRepository
    {
       
        public Task<List<Command>> GetAllCommanAsync()
        {
            throw new NotImplementedException();
        }
    }
}
