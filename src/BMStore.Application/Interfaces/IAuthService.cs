using BMStore.Application.Models;

using Microsoft.AspNetCore.Authentication;

namespace BMStore.Application.Interfaces;

public interface IAuthService
{
    Task<TokenResponse> AuthenticateAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken);
    
    Task<TokenResponse> AuthenticateGoogleAsync(CancellationToken cancellationToken);

    (AuthenticationProperties, string) PrepareGoogleLoginProperties(string? returnUrl);
}
