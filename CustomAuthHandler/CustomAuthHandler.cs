using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CustomAuthHandler
{

    public class CustomSchemeOptions : AuthenticationSchemeOptions
    {
    }

    public class CustomAuthHandler : AuthenticationHandler<CustomSchemeOptions>
    {

        ICustomAuthManager _authManager;
        public CustomAuthHandler(IOptionsMonitor<CustomSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, ICustomAuthManager authmanager)
            : base(options, logger, encoder, clock)
        {
            _authManager = authmanager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Authorization bearer <token>
            var token = Request.Headers["Authorization"].ToString();
            // var body = Request.Body;
            if (string.IsNullOrEmpty(token)) return null;
            var bearerValue = token.Substring("bearer".Length).Trim();


            List<Claim> clms = new List<Claim>();
            clms.Add(new Claim(ClaimTypes.Name, _authManager.ValidUsers[bearerValue]));

            var identity = new ClaimsIdentity(clms, "Custom");
            var principle = new GenericPrincipal(identity, null);
            var authenticnTicket = new AuthenticationTicket(principle, "Cstm");
            return AuthenticateResult.Success(authenticnTicket);


        }
    }
}
