
using Microsoft.AspNetCore.Localization;

namespace Website.Middlwares
{
    public class SetDefaultCulture : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fa")),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            await next(context);
        }
    }
}
