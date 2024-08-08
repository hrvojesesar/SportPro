using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SportPro.Web
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                if (httpContext.Request.Path == "/Account/Login")
                {
                    httpContext.Response.Redirect("/Home/Index");
                    return;
                }
            }
            else
            {
                if (httpContext.Request.Path != "/Account/Login" && httpContext.Request.Path != "/Account/AccessDenied")
                {
                    httpContext.Response.Redirect("/Account/Login");
                    return;
                }
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RedirectMiddlewareExtensions
    {
        public static IApplicationBuilder UseRedirectMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RedirectMiddleware>();
        }
    }
}
