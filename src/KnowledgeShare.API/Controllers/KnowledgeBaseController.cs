using KnowledgeShare.API.Services;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeBaseController : ControllerBase
    {
        private readonly IKnowledgeBaseService _knowledgeBaseService;
        private readonly IVoteService _voteService;
        private readonly IReportService _reportService;
        public KnowledgeBaseController(IKnowledgeBaseService knowledgeBaseService, IVoteService voteService)
        {
            _knowledgeBaseService = knowledgeBaseService;
            _voteService = voteService;
        }

        #region Knowledge

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

        #endregion

        #region Comment

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
        public async Task<IActionResult> CreateComment(int knowledgeBaseId, [FromBody] CommentCreateRequest request)
        {
            var result = await _knowledgeBaseService.CreateCommentAsync( knowledgeBaseId, request);

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

        #region Reports
        [HttpGet("{knowledgeBaseId}/reports/filter")]
        public async Task<IActionResult> GetReportsPaging(int knowledgeBaseId, string filter, int pageIndex, int pageSize)
        {
            var result = await _reportService.GetAllReportsAsync(knowledgeBaseId, filter, pageIndex, pageSize);

            return Ok(result);
        }

        [HttpGet("{knowledgeBaseId}/reports/{reportId}")]
        public async Task<IActionResult> GetCommentDetail(int knowledgeBaseId, int reportId)
        {
            var result = await _reportService.GetReportDetail(knowledgeBaseId, reportId);

            return Ok(result);
        }

        [HttpPost("{knowledgeBaseId}/reports")]
        public async Task<IActionResult> PostReport(int knowledgeBaseId, [FromBody]ReportCreateRequest report)
        {
            var result = await _reportService.CreateReportAsync(knowledgeBaseId, report);

            return Ok("Created successfully!");
        }

        [HttpPut("{knowledgeBaseId}/reports/{reportId}")]
        public async Task<IActionResult> PutReport(int knowledgeBaseId, [FromBody] ReportCreateRequest report)
        {
            var userName = User.Identity!.Name;
            var result = await _reportService.UpdateReportAsync(knowledgeBaseId, userName, report);
            return Ok("Update successfully!");
        }

        [HttpDelete("{knowledgeBaseId}/reports/{reportId}")]
        public async Task<IActionResult> DeleteReport(int knowledgeBaseId, int reportId)
        {
            var result = await _reportService.DeleteReportAsync(knowledgeBaseId, reportId);

            if (!result)
            {
                return BadRequest("Report không tồn tại");
            }
            return Ok("Deleted successfully!");
        }
        #endregion  
    }
}
