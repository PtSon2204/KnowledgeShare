using KnowledgeShare.ViewModels.Content;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    public partial class KnowledgeBaseController
    {
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
        public async Task<IActionResult> PostReport(int knowledgeBaseId, [FromBody] ReportCreateRequest report)
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
