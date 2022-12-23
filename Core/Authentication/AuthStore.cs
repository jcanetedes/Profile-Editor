using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using ProfileEditor.Core.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Security.Claims;

namespace ProfileEditor.Core.Authentication;



public class AuthStore : AuthenticationStateProvider, IAuthStore
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    public AuthStore(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }
    public async Task SetToken(TokenDto token)
    {
        ClaimsPrincipal claimsPrincipal;

        if (token is not null)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.ReadJwtToken(token.Token);

            var claim = jwtSecurityToken.Claims.FirstOrDefault();
            var roleClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type.Contains("role"));

            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, claim.Value),
                new Claim(ClaimTypes.Role, roleClaim.Value)
            }, "CustomAuth"));

            await _sessionStorage.SetAsync("user", token);
        }
        else
        {
            await _sessionStorage.DeleteAsync("user");
            claimsPrincipal = _anonymous;
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<TokenDto>("user");

            var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

            if (userSession == null)
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.ReadJwtToken(userSession.Token);

            var claim = jwtSecurityToken.Claims.FirstOrDefault();
            var roleClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type.Contains("role"));

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, claim.Value),
                new Claim(ClaimTypes.Role, roleClaim.Value)
            }, "CustomAuth"));

            //var claimsPrincipal = new ClaimsPrincipal();

            //claimsPrincipal.AddIdentity(new ClaimsIdentity(claims));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }
    public async Task<string> GetBearerToken()
    {
        try
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<TokenDto>("user");

            var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

            if (userSession == null)
                return "";
            
            return userSessionStorageResult.Value.Token;
        }
        catch(Exception ex)
        {
            return "";
        }
    }

}
public interface IAuthStore
{
    Task SetToken(TokenDto token);
    Task<string> GetBearerToken();
}
