using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KnowledgeShare.API.Helpers
{
    //hỗ trợ bắn ra lỗi theo format custom mình đã cấu hình trong fluent validation
    public class ApiValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(context.ModelState));
            }
            base.OnActionExecuting(context);
        }
    }
}
