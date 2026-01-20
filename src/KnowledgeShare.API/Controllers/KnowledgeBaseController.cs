using KnowledgeShare.API.Services;
using KnowledgeShare.ViewModels.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeBaseController : ControllerBase
    {
        private readonly KnowledgeBaseService _knowledgeBaseService;

        public KnowledgeBaseController(KnowledgeBaseService knowledgeBaseService)
        {
            _knowledgeBaseService = knowledgeBaseService;
        }

        [HttpPost]
        public async Task<IActionResult> PostKnowledgeBase([FromBody] CreateKnowledgeBaseRequest request)
        {
            var result = await _knowledgeBaseService.CreateKnowledgeBaseRequestAsync(request);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKnowledgeBase()
        {
            var list = await _knowledgeBaseService.GetAllKnowledgeBaseRequestsAsync();

            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnowledgeBase(int id)
        {
            var result = await _knowledgeBaseService.DeleteKnowledgeBaseRequestAsync(id);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKnowledgeBaseById(int id)
        {
            var result = await _knowledgeBaseService.GetKnowledgeBaseRequestByIdAsync(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKnowledgeBase(int id, [FromBody] CreateKnowledgeBaseRequest request)
        {
            var result = await _knowledgeBaseService.UpdateKnowledgeBaseRequestAsync(id, request);

            return Ok(result);
        }

        [HttpGet("{knowledgeBaseId}/comments/keyword")]
        public async Task<IActionResult> GetCommentPaging(int knowledgeBaseId, string keyword, int pageIndex, int pageSize)
        {
            var result = await _knowledgeBaseService.GetAllCommentPaging(knowledgeBaseId, keyword, pageIndex, pageSize);

            return Ok(result);
        }

        [HttpGet("{knowledgeBaseId}/comments/{commentId}")]
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
        public async Task<IActionResult> CreateComment( [FromBody] CommentCreateRequest request)
        {
            var result = await _knowledgeBaseService.CreateCommentAsync( request);

            if (result == null)
            {
                return BadRequest();
            }
            
            return Ok("Created successfully!");
        }

        [HttpPut("{knowledgeBaseId}/comments/{commentId}")]
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
        public async Task<IActionResult> DeleteComment( int commentId)
        {
            var result = await _knowledgeBaseService.DeleteCommentAsync(commentId);

            if (!result)
            {
                return NotFound();
            }

            return Ok("Delete successfully!");
        }
    }
}
