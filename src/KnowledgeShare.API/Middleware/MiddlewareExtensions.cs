namespace KnowledgeShare.API.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorWrapping(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorWrappingMiddleware>();
        } 
    }
}
