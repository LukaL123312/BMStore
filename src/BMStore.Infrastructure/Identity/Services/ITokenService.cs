using BMStore.Infrastructure.Identity.Models.Authentication;

namespace BMStore.Infrastructure.Identity.Services;
/// <summary>
///     A collection of token related services
/// </summary>
public interface ITokenService
{
    /// <summary>
    ///     Validate the credentials entered when logging in.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress);
}
