using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class AccountController : Controller
    {

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
