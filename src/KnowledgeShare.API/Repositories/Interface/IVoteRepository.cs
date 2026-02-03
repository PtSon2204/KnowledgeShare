namespace KnowledgeShare.API.Repositories.Interface
{
    public interface IVoteRepository
    {
        Task<Vote> CreateVoteAsync(Vote vt);
        Task<Vote> UpdateVoteAsync(Vote vt);
        Task<bool> DeleteVoteAsync(Vote vt);
        Task<List<Vote>> GetAllVotesAsync(int knowledgeId);
        Task<Vote> GetVoteByIdAsync(int knowledgeId);
        Task<Vote> FindVoteAsync(int knowledgeId, string userId);
    }
}
