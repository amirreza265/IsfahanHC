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
    public class ShopSeller
    {
        private readonly RequestDelegate _next;

        public ShopSeller(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            if (httpContext.Request.Path.StartsWithSegments("/seller"))
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    httpContext.Response.Redirect("/Account/login");
                }
                else if (!bool.Parse(httpContext.User.FindFirstValue("IsSeller")))
                {
                    httpContext.Response.Redirect("/Account/AccessDenied");
                }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ShopSellerExtensions
    {
        public static IApplicationBuilder UseShopSeller(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShopSeller>();
        }
    }
}
