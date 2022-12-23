using Microsoft.AspNetCore.Mvc;
using ProfileEditor.Core.DTOs;
using Refit;

namespace ProfileEditor.Core.Services;

public interface IProfileService
{
    [Get("/api/v1/profile")]
    Task<ProfileDto> GetProfile([Header("Authorization")] string auth);

    [Put("/api/v1/profile")]
    Task<ProfileDto> UpdateProfile([Body]ProfileEditDto profile, [Header("Authorization")] string auth);
}
