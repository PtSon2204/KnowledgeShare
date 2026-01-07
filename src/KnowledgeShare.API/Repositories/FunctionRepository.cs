using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FunctionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Function> CreateFunctionAsync(Function func)
        {
             _dbContext.Functions.Add(func);
             await _dbContext.SaveChangesAsync();
            return func;
        }

        public async Task<Function> DeleteFunctionAsync(Function func)
        {
            _dbContext.Functions.Remove(func);
            await _dbContext.SaveChangesAsync();
            return func;
        }

        public async Task<List<Function>> GetAllFunctionsAsync()
        {
            return await _dbContext.Functions.AsNoTracking().ToListAsync();
        }

        public async Task<Function> GetFunctionByIdAsync(string id)
        {
            return await _dbContext.Functions.FindAsync(id);
        }

        public async Task<Function> UpdateFunctionAsync(Function func)
        {
            _dbContext.Functions.Update(func);
            await _dbContext.SaveChangesAsync();
            return func;
        }
    }
}
