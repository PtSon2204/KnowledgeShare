using KnowledgeShare.API.Authorization;
using KnowledgeShare.API.Constants;
using KnowledgeShare.ViewModels.Content;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    public partial class KnowledgeBaseController
    {
        #region Comment

        [HttpGet("{knowledgeBaseId}/comments/keyword")]
        [ClaimRequirement(FunctionCode.CONTENT_COMMENT, CommandCode.VIEW)]
        public async Task<IActionResult> GetCommentPaging(int knowledgeBaseId, string keyword, int pageIndex, int pageSize)
        {
            var result = await _knowledgeBaseService.GetAllCommentPaging(knowledgeBaseId, keyword, pageIndex, pageSize);

            return Ok(result);
        }

        [HttpGet("{knowledgeBaseId}/comments/{commentId}")]
        [ClaimRequirement(FunctionCode.CONTENT_COMMENT, CommandCode.VIEW)]
        public async Task<IActionResult> GetCommentDetail(int commentId)
        {
            var comment = await _knowledgeBaseService.GetCommentVmByIdAsync(commentId);

            if (comment == null)
            {
                return BadRequest("Not found");
            }
            return Ok(comment);
        }

        [HttpPost("{knowledgeBaseId}/comments")]
        [ClaimRequirement(FunctionCode.CONTENT_COMMENT, CommandCode.CREATE)]
        public async Task<IActionResult> CreateComment(int knowledgeBaseId, [FromBody] CommentCreateRequest request)
        {
            var result = await _knowledgeBaseService.CreateCommentAsync(knowledgeBaseId, request);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok("Created successfully!");
        }

        [HttpPut("{knowledgeBaseId}/comments/{commentId}")]
        [ClaimRequirement(FunctionCode.CONTENT_COMMENT, CommandCode.UPDATE)]
        public async Task<IActionResult> UpdateComment(int commentId, CommentCreateRequest request)
        {
            var userName = User.Identity!.Name;

            var result = await _knowledgeBaseService.UpdateCommentAsync(
                commentId,
                request,
                userName);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok("Update successfully!");
        }

        [HttpDelete("{knowledgeBaseId}/comments/{commentId}")]
        [ClaimRequirement(FunctionCode.CONTENT_COMMENT, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteComment(int knowledgeBaseId, int commentId)
        {
            var result = await _knowledgeBaseService.DeleteCommentAsync(knowledgeBaseId, commentId);

            if (!result)
            {
                return NotFound();
            }

            return Ok("Delete successfully!");
        }
        #endregion






    }
}
