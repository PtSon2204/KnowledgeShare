using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace KnowledgeShare.API.Helpers
{
    public class ApiNotFoundResponse : ApiResponse
    {
        public ApiNotFoundResponse(string message) : base(404,message) { }
    }
}

