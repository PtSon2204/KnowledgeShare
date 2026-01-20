using System.ComponentModel.Design;
using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KnowledgeShare.API.Repositories
{
    public class CommandInFunctionRepository : ICommandsInFunctionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommandInFunctionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandInFunction> CreateCommandToFunction(CommandInFunction commandInFunction)
        {
            await _dbContext.CommandInFunctions.AddAsync(commandInFunction);
            await _dbContext.SaveChangesAsync();
            return commandInFunction;
        }

        public async Task<bool> DeleteCommandToFunction(CommandInFunction commandInFunction)
        {
            _dbContext.CommandInFunctions.Remove(commandInFunction);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string commandId, string functionId)
        {
            return await _dbContext.CommandInFunctions.AnyAsync(x => x.CommandId == commandId && x.FunctionId == functionId);
        }

        public async Task<CommandInFunction> FindCommandInFunction(string commandId, string functionId)
        {
            return await _dbContext.CommandInFunctions.FindAsync(commandId, functionId);
        }

     

        public async Task<List<Entities.Command>> GetCommandsInFunction(string functionId)
        {
            return await _dbContext.Commands
                         .Where(c => _dbContext.CommandInFunctions
                         .Any(cif => cif.CommandId == c.Id && cif.FunctionId == functionId))
                         .Select(c => new Entities.Command
                         {
                             Id = c.Id,
                             Name = c.Name
                         }).ToListAsync();
        }

        public async Task<List<Entities.Command>> GetCommandsNotInFunction(string functionId)
        {
            return await _dbContext.Commands
                        .Where(c => !_dbContext.CommandInFunctions
                        .Any(cif => cif.CommandId == c.Id && cif.FunctionId == functionId))
                        .Select(c => new Entities.Command
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToListAsync();
        }

    }
}
