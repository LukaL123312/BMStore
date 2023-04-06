using BMStore.Application.Interfaces;
using BMStore.Application.Models;
using BMStore.Domain.Entities;
using BMStore.Infrastructure.Identity.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BMStore.Infrastructure.Identity.Services;

public class TokenService : ITokenService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly Token _token;
    private readonly HttpContext _httpContext;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<Token> tokenOptions,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _token = tokenOptions.Value;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public async Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress)
    {
        if (await IsValidUser(request.Username, request.Password))
        {
            ApplicationUser user = await GetUserByUserName(request.Username);

            if (user != null && user.IsEnabled)
            {
                var userDto = new UserEntity
                {
                    IdentityId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email
                };
                string role = (await _userManager.GetRolesAsync(user))[0];
                string jwtToken = await GenerateJwtToken(user);

                await _userManager.UpdateAsync(user);

                return new TokenResponse(userDto,
                                         role,
                                         jwtToken
                                         //""//refreshToken.Token
                                         );
            }
        }

        return null;
    }

    private async Task<bool> IsValidUser(string username, string password)
    {
        ApplicationUser user = await GetUserByUserName(username);

        if (user == null)
        {
            // Username or password was incorrect.
            return false;
        }

        SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);

        return signInResult.Succeeded;
    }

    private async Task<ApplicationUser> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
    private async Task<ApplicationUser> GetUserByUserName(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        string role = (await _userManager.GetRolesAsync(user))[0];
        byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);

        JwtSecurityTokenHandler handler = new();
        SecurityTokenDescriptor descriptor = new()
        {
            Issuer = _token.Issuer,
            Audience = _token.Audience,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", user.Id),
                new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}
