using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;

        public AccountController(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
            _httpContext = _httpContextAccessor.HttpContext;
        }

        public async Task<IActionResult> GetAccessToken(CancellationToken ct)
        {
            var accessToken = await _httpContext.GetTokenAsync("access_token");
            return Ok(new { token = accessToken });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return new SignOutResult
            {
                AuthenticationSchemes = [ OpenIdConnectDefaults.AuthenticationScheme , CookieAuthenticationDefaults.AuthenticationScheme
                ],
            };
        }
    }
}
