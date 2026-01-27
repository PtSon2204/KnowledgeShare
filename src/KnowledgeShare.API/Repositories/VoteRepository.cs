using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VoteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vote> CreateVoteAsync(Vote vt)
        {
            _dbContext.Votes.Add(vt);
            await _dbContext.SaveChangesAsync();
            return vt;
        }

        public async Task<bool> DeleteVoteAsync(Vote vt)
        {
            _dbContext.Votes.Remove(vt);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Vote> FindVoteAsync(int knowledgeId, string userId)
        {
            return await _dbContext.Votes.FindAsync(knowledgeId, userId);
        }

        public async Task<List<Vote>> GetAllVotesAsync(int knowledgeId)
        {
            return await _dbContext.Votes.Where(x => x.KnowledgeBaseId == knowledgeId).AsNoTracking().ToListAsync();
        }

        public  Task<Vote> GetVoteByIdAsync(int knowledgeId)
        {
            throw new NotImplementedException();
        }

        public Task<Vote> UpdateVoteAsync(Vote vt)
        {
            throw new NotImplementedException();
        }
    }
}
