
using Microsoft.AspNetCore.Localization;
using System.Security.Claims;
using Website.Externals.Refit;

namespace Website.Midlewares
{
    public class SetUserCulture : IMiddleware
    {
        private readonly IUserService _userService;

        public SetUserCulture(IUserService userService)
        {
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userId = context.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userService.GetUserConfigs(userId, default);
            if (user is not null && user.Content is not null) SetCultureInCookie(context, user);
            await next(context);
        }

        private static void SetCultureInCookie(HttpContext context, Refit.ApiResponse<Dtos.UserDto>? user)
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(user.Content.Culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
        }
    }
}
