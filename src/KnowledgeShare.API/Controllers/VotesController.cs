using KnowledgeShare.ViewModels.Content;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    public partial class KnowledgeBaseController
    {
        #region Votes
        [HttpPost("{knowledgeId}/votes")]
        public async Task<IActionResult> PostVoteRequest(int knowledgeId, [FromBody] VoteCreateRequest request)
        {
            var result = await _voteService.CreateVoteVmAsync(knowledgeId, request);

            if (result == null)
            {
                return BadRequest();
            }
            return Ok("Created successfully!");
        }

        [HttpDelete("{knowledgeBaseId}/votes/{userId}")]
        public async Task<IActionResult> DeleteVote(int knowledgeBaseId, string userId)
        {
            var result = await _voteService.DeleteVoteVmAsync(knowledgeBaseId, userId);

            if (!result)
            {
                return NotFound();
            }
            return Ok("Delete successfully!");
        }
        #endregion
    }
}
