using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class CommandInFunctionRepository : ICommandsInFunctionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommandInFunctionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Command>> GetCommandsInFunction(string functionId)
        {
            return await _dbContext.Commands
                         .Where(c => _dbContext.CommandInFunctions
                         .Any(cif => cif.CommandId == c.Id && cif.FunctionId == functionId))
                         .Select(c => new Command
                         {
                             Id = c.Id,
                             Name = c.Name
                         }).ToListAsync();
        }

        public async Task<List<Command>> GetCommandsNotInFunction(string functionId)
        {
            return await _dbContext.Commands
                        .Where(c => !_dbContext.CommandInFunctions
                        .Any(cif => cif.CommandId == c.Id && cif.FunctionId == functionId))
                        .Select(c => new Command
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToListAsync();
        }
    }
}
