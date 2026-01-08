using KnowledgeShare.API.Services;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionsController : ControllerBase
    {
        private readonly IFunctionService _functionService;
        private readonly ICommandInFunctionService _commandInFunctionService;
        public FunctionsController(IFunctionService functionService, ICommandInFunctionService commandInFunctionService)
        {
            _functionService = functionService;
            _commandInFunctionService = commandInFunctionService;
        }

        [HttpPost]
        public async Task<IActionResult> PostFunction([FromBody] FunctionVm functionVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _functionService.CreateFunctionVmAsync(functionVm);
            if (result != null)
            {
                return Ok("Create successfully!");
            }
            else
            {
                return BadRequest("Created fail!");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var function = await _functionService.GetFunctionVmByIdAsync(id);

            if (function == null)
            {
                return NotFound();
            }

            return Ok(function);
        }

        //URL: PUT:  http://localhost:7122/api/roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id, [FromBody] FunctionVm functionVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _functionService.UpdateFunctionVmAsync(id, functionVm);
            if (result != null)
            {
                return Ok("Update successfully!");
            }
            else
            {
                return BadRequest("Not found!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeteleFunction(string id)
        {
            var result = await _functionService.DeleteFunctionVmAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllFunctions()
        {
            var functions = await _functionService.GetAllFunctionVmsAsync();
            
            if (functions == null)
            {
                return NotFound();
            }
            return Ok(functions);
        }

        [HttpGet("{functionId}/commands-in-function")]
        public async Task<IActionResult> GetCommandInFunction(string functionId)
        {
            var data = await _commandInFunctionService.GetCommandsInFunction(functionId);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("{functionId}/commands-not-in-function")]
        public async Task<IActionResult> GetCommandNotInFunction(string functionId)
        {
            var data = await _commandInFunctionService.GetCommandsInFunction(functionId);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
