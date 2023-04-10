using BMStore.Application.Models;

namespace BMStore.Application.Interfaces;

public interface IAuthService
{
    Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress);
    
    Task<TokenResponse> AuthenticateGoogle();
}
