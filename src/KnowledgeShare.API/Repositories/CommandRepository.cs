using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class CommandRepository : ICommanRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommandRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Command>> GetAllCommanAsync()
        {
            return await _dbContext.Commands.AsNoTracking().ToListAsync();
        }
    }
}
