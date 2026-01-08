using KnowledgeShare.API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _commandService;


        public CommandController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListCommand()
        {
            var commands = await _commandService.GetAllCommandVmAsync();

            if (commands == null)
            {
                return NotFound();
            }

            return Ok(commands);
        }
    }
}
