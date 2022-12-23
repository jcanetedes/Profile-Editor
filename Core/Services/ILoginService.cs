using ProfileEditor.Core.DTOs;
using Refit;

namespace ProfileEditor.Core.Services;

public interface ILoginService
{
    [Post("/api/v1/auth/UserLogin")]
    Task<TokenDto> LoginAsync(LoginDto login);
}
