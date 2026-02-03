using KnowledgeShare.API.Authorization;
using KnowledgeShare.API.Constants;
using KnowledgeShare.API.Services;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class KnowledgeBaseController : ControllerBase
    {
        private readonly IKnowledgeBaseService _knowledgeBaseService;
        private readonly IVoteService _voteService;
        private readonly IReportService _reportService;
        private readonly IAttachmentService _attachmentService;
        private readonly ILogger<KnowledgeBaseController> _logger;
        public KnowledgeBaseController(IKnowledgeBaseService knowledgeBaseService, IVoteService voteService, IAttachmentService attachmentService, IReportService reportService, ILogger<KnowledgeBaseController> logger)
        {
            _knowledgeBaseService = knowledgeBaseService;
            _voteService = voteService;
            _attachmentService = attachmentService;
            _reportService = reportService;
            _logger = logger;
        }

        #region Knowledge

        [HttpPost]
        [ClaimRequirement(FunctionCode.CONTENT_KNOWLEDGEBASE, CommandCode.CREATE)]
        public async Task<IActionResult> PostKnowledgeBase([FromForm] CreateKnowledgeBaseRequest request)
        {
            _logger.LogInformation("Begin PostKnowledge");
            var result = await _knowledgeBaseService.CreateKnowledgeBaseRequestAsync(request);

            _logger.LogInformation("End Postknowledge API - success");

            return Ok(result);
        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.CONTENT_KNOWLEDGEBASE, CommandCode.VIEW)]
        public async Task<IActionResult> GetAllKnowledgeBase()
        {
            var list = await _knowledgeBaseService.GetAllKnowledgeBaseRequestsAsync();

            return Ok(list);
        }

        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.CONTENT_KNOWLEDGEBASE, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteKnowledgeBase(int id)
        {
            var result = await _knowledgeBaseService.DeleteKnowledgeBaseRequestAsync(id);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.CONTENT_KNOWLEDGEBASE, CommandCode.VIEW)]
        public async Task<IActionResult> GetKnowledgeBaseById(int id)
        {
            var result = await _knowledgeBaseService.GetKnowledgeBaseRequestByIdAsync(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.CONTENT_KNOWLEDGEBASE, CommandCode.UPDATE)]
        public async Task<IActionResult> PutKnowledgeBase(int id, [FromBody] CreateKnowledgeBaseRequest request)
        {
            var result = await _knowledgeBaseService.UpdateKnowledgeBaseRequestAsync(id, request);

            return Ok(result);
        }

        #endregion

      
    }
}
