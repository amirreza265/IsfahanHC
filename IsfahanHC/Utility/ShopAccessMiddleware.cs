using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IsfahanHC.Utility
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ShopAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public ShopAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/admin"))
            {
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    httpContext.Response.Redirect("/Account/login");
                }
                else if (!bool.Parse(httpContext.User.FindFirstValue("IsAdmin")))
                {
                    httpContext.Response.Redirect("/AccessDenied");
                }
            }
            else if (httpContext.Request.Path.StartsWithSegments("/seller"))
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    httpContext.Response.Redirect("/Account/login");
                }
                else if (!bool.Parse(httpContext.User.FindFirstValue("IsSeller")))
                {
                    httpContext.Response.Redirect("/AccessDenied");
                }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ShopAccessMiddlewareExtensions
    {
        public static IApplicationBuilder UseShopAccess(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShopAccessMiddleware>();
        }
    }
}
