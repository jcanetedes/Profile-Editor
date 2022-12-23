using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace ProfileEditor.Core.Authentication
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly AuthenticationStateProvider authTokenStore;

        public AuthHandler(AuthenticationStateProvider authTokenStore)
        {
            this.authTokenStore = authTokenStore ?? throw new ArgumentNullException(nameof(authTokenStore));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authStateProvider = (AuthStore)authTokenStore;

            var token = await authStateProvider.GetBearerToken();

            //potentially refresh token here if it has expired etc.

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
