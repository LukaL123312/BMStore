using BMStore.Application.Models;

namespace BMStore.Application.Interfaces;

public interface ITokenService
{
    Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress);
}
