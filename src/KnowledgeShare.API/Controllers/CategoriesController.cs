using KnowledgeShare.API.Authorization;
using KnowledgeShare.API.Constants;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [ClaimRequirement(FunctionCode.CONTENT_CATEGORY, CommandCode.CREATE)] 
        
        public async Task<IActionResult> PostCategory([FromBody] CategoryCreateRequest request)
        {
            var result = await _categoryService.CreateCategoryCreateRequestAsync(request);

            if (result == null)
            {
                return NotFound();
            }

            return Ok("Create successfully!");
        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.CONTENT_CATEGORY, CommandCode.VIEW)]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategoryCreateRequestAllAsync();

            return Ok(result);
        }

        [HttpGet("id")]
        [ClaimRequirement(FunctionCode.CONTENT_CATEGORY, CommandCode.VIEW)]
        public async Task<IActionResult> GetCategoryById(int cateId)
        {
            var result = await _categoryService.GetCategoryCreateRequestAsync(cateId);

            return Ok(result);
        }

        [HttpPut]
        [ClaimRequirement(FunctionCode.CONTENT_CATEGORY, CommandCode.UPDATE)]
        public async Task<IActionResult> PutCategory(int cateId, [FromBody] CategoryCreateRequest request)
        {
            var result = await _categoryService.UpdateCategoryCreateRequestAsync(cateId, request);

            if (result == null)
            {
                return NotFound();
            }

            return Ok("Update successfully!");
        }

        [HttpDelete("id")]
        [ClaimRequirement(FunctionCode.CONTENT_CATEGORY, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteCategoryById(int cateId)
        {
            var result = await _categoryService.DeleteCategoryCreateRequestAsync(cateId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok("Delete successfully");
        }
    }
}
