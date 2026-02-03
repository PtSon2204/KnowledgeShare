using KnowledgeShare.API.Authorization;
using KnowledgeShare.API.Constants;
using KnowledgeShare.API.Helpers;
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
        private readonly ILogger<FunctionsController> _logger;
        public FunctionsController(IFunctionService functionService, ICommandInFunctionService commandInFunctionService, ILogger<FunctionsController> logger)
        {
            _functionService = functionService;
            _commandInFunctionService = commandInFunctionService;
            _logger = logger;
        }

        [HttpPost]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.CREATE)]
        [ApiValidationFilter]
        public async Task<IActionResult> PostFunction([FromBody] FunctionVm functionVm)
        {
            _logger.LogInformation("Begin PostFunction API");
            var result = await _functionService.CreateFunctionVmAsync(functionVm);

            if (result == null)
            {
                _logger.LogInformation("Post function api - failed");

                return BadRequest(new ApiBadRequestResponse(
                    $"Function with id {functionVm.Id} already exists or creation failed"
                ));
            }

            _logger.LogInformation("Post function api - success");
            return Ok(new ApiOkResponse(result));
        }

        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
        [ApiValidationFilter]
        public async Task<IActionResult> GetById(string id)
        {
            var function = await _functionService.GetFunctionVmByIdAsync(id);

            if (function == null)
            {
                return NotFound(new ApiNotFoundResponse($"Cannot found with id {id}"));
            }

            return Ok(new ApiOkResponse(function));
        }

        //URL: PUT:  http://localhost:7122/api/roles/{id}
        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.UPDATE)]
        [ApiValidationFilter]
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
                return BadRequest(new ApiBadRequestResponse("Put function failed!"));
            }
        }

        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.DELETE)]
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
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
        public async Task<IActionResult> GetAllFunctions()
        {
            var functions = await _functionService.GetAllFunctionVmsAsync();
            
            if (functions == null)
            {
                return NotFound();
            }
            return Ok(functions);
        }

        [HttpGet("{functionId}/commands")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
        public async Task<IActionResult> GetCommandInFunction(string functionId)
        {
            var data = await _commandInFunctionService.GetCommandsInFunction(functionId);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("{functionId}/commands/not-in-function")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
        public async Task<IActionResult> GetCommandNotInFunction(string functionId)
        {
            var data = await _commandInFunctionService.GetCommandsInFunction(functionId);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("{functionId}/commands")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.UPDATE)]
        public async Task<IActionResult> PostCommandToFunction([FromBody] CommandInFunctionVm commandInFunctionVm)
        {
            var result = await _commandInFunctionService.CreateCommandToFunction(commandInFunctionVm);
            if (result == null)
            {
                return BadRequest(new ApiBadRequestResponse("Put command to function failed!"));
            }

            return Ok("Created successfully!");
        }

        [HttpDelete("{functionId}/commands/{commandId}")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteCommandToFunction(string commandId, string functionId)
        {
            var result = await _commandInFunctionService.DeleteCommandToFunction(commandId, functionId);

            if (!result)
            {
                return NotFound();
            }
            return Ok("Deleted!");
        }
    }
}
