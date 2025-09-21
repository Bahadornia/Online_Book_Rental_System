
using Microsoft.AspNetCore.Authentication;

namespace Website.Externals
{
    public class UserDelegatingHandler: DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpContext _context;

        public UserDelegatingHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _context = _contextAccessor.HttpContext;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.Headers.Authorization =
                  new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
