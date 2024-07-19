using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace EclipseTasks.Api.Authentication
{
    public class SimpleAuthenticationHandler : AuthenticationHandler<SimpleAuthenticationOptions>
    {
        public SimpleAuthenticationHandler(
            IOptionsMonitor<SimpleAuthenticationOptions> options,
            ILoggerFactory loggerFactory,
            UrlEncoder encoder, ISystemClock systemClock) : base(options, loggerFactory, encoder, systemClock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var userKey = Request.Headers.ContainsKey(Options.TokenHeaderName)
                ? Request.Headers[Options.TokenHeaderName].ToString()
                : "anonymous";

            var role = userKey.StartsWith("gestor") ? "administator" : "commum";

            var claims = new List<Claim>()
            {
                new Claim("UserKey", userKey),
                new Claim(ClaimTypes.Role, role),
            };

            var claimsIdentity = new ClaimsIdentity(claims, this.Scheme.Name);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, this.Scheme.Name));
        }
    }

    public class SimpleAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "SimplesAuthenticationScheme";
        public string TokenHeaderName { get; set; } = "Authentication";
    }
}