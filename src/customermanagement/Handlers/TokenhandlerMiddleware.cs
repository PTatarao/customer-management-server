using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace customermanagement.Handlers
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TokenhandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenhandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value?.Contains("Login/") == false)
            {
                return _next(httpContext);
            }
            if (httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token = httpContext.Request.Headers["Authorization"];
                bool userExists = ValidateToken(token);
                if(userExists)
                {
                    httpContext.Response.StatusCode = 401; // Unauthorized
                    return httpContext.Response.WriteAsync("Access Denied: No Token Provided");
                }
            }
            return _next(httpContext);
        }
        private bool ValidateToken(string token)
        {
            var user = SessioStorage.Get(token);
            if (string.IsNullOrEmpty(user))
            {
                return false;
            }
            return true;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TokenhandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenhandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenhandlerMiddleware>();
        }
    }
}
