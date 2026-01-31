using KnowledgeShare.API.Constants;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeShare.API.Authorization
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(FunctionCode functionId, CommandCode commandId) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { functionId, commandId };  
        }
    }
}
