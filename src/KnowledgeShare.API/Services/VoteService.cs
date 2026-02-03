using KnowledgeShare.API.Data;
using KnowledgeShare.API.Repositories.Entities;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.Content;

namespace KnowledgeShare.API.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly ApplicationDbContext _dbContext;

        public VoteService(IVoteRepository voteRepository, ApplicationDbContext applicationDb)
        {
            _voteRepository = voteRepository;
            _dbContext = applicationDb;
        }

        public async Task<VoteCreateRequest> CreateVoteVmAsync(int knowledgeBaseId, VoteCreateRequest request)
        {
            var vote = await _voteRepository.FindVoteAsync(knowledgeBaseId, request.UserId);

            var newVote = new Vote
            {
                KnowledgeBaseId = vote.KnowledgeBaseId,
                UserId = vote.UserId,
            };
            await _voteRepository.CreateVoteAsync(newVote);

            var knowledgeBase = await _dbContext.KnowledgeBases.FindAsync(knowledgeBaseId); 
            if (knowledgeBase != null)
            {
                return null;
            }
            knowledgeBase.NumberOfVotes = knowledgeBase.NumberOfVotes.GetValueOrDefault(0) + 1;
            _dbContext.KnowledgeBases.Update(knowledgeBase);
            _dbContext.SaveChanges();

            return request;
        }

        public async Task<bool> DeleteVoteVmAsync(int knowledgeId, string userId)
        {
            var vote = await _voteRepository.FindVoteAsync(knowledgeId, userId);
            await _voteRepository.DeleteVoteAsync(vote);

            var knowledgeBase = await _dbContext.KnowledgeBases.FindAsync(knowledgeId);
            if (knowledgeBase != null)
            {
                return false;
            }
            knowledgeBase.NumberOfVotes = knowledgeBase.NumberOfVotes.GetValueOrDefault(0) - 1;
            _dbContext.KnowledgeBases.Update(knowledgeBase);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<List<VoteVm>> GetAllVotesAsync(int knowledgeId)
        {
            var vote = await _voteRepository.GetAllVotesAsync(knowledgeId);

            return vote.Select(v => new VoteVm
            {
                UserId = v.UserId,
                KnowledgeBaseId = v.KnowledgeBaseId,
                CreateDate = v.CreateDate,
                LastModifiedDate = v.LastModifiedDate,
            }).ToList();
        }

        public Task<VoteVm> GetVoteByIdAsync(int knowledgeId)
        {
            throw new NotImplementedException();
        }
    }
}
