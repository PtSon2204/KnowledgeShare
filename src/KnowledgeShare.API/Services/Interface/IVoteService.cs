using KnowledgeShare.ViewModels.Content;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.API.Services.Interface
{
    public interface IVoteService
    {
        Task<VoteVm> GetVoteByIdAsync(int knowledgeId);
        Task<List<VoteVm>> GetAllVotesAsync(int knowledgeId);
        Task<VoteCreateRequest> CreateVoteVmAsync(int knowledgeBaseId, VoteCreateRequest request);
        Task<bool> DeleteVoteVmAsync(int knowledgeId, string userId);
    }
}
