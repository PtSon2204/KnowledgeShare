using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    public partial class KnowledgeBaseController
    {
        #region Attachments
        [HttpGet("{knowledgeBaseId}/attachments")]
        public async Task<IActionResult> GetAttachment(int knowledgeBaseId)
        {
            var result = await _attachmentService.GetListAttachmentAsync(knowledgeBaseId);

            return Ok(result);
        }

        [HttpDelete("{knowledgeBaseId}/attachments/{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(int attachmentId)
        {
            var result = await _attachmentService.DeleteAttachmentAsync(attachmentId);

            return Ok("Deleted successfully!");
        }


        #endregion
    }
}
