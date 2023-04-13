using BMStore.Application.Interfaces;
using BMStore.Application.Models;
using BMStore.Domain.Constants;
using BMStore.Domain.Entities;
using BMStore.Infrastructure.Identity.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace BMStore.Infrastructure.Identity.Services;

public class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly Token _token;
    private readonly HttpContext _httpContext;

    public AuthService(
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

    public async Task<TokenResponse> AuthenticateAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (await IsValidUser(request.Username, request.Password))
        {
            ApplicationUser user = await GetUserByUserName(request.Username);

            if (user != null && user.IsEnabled)
            {
                return await PrepareJwt(user);
            }
        }

        return null;
    }

    public async Task<TokenResponse> AuthenticateGoogleAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();

        if (externalLoginInfo is null)
        {
            throw new AuthenticationException("External login was not found.");
        }

        string loginProvider = externalLoginInfo.LoginProvider;
        string providerKey = externalLoginInfo.ProviderKey;

        var signInResult = await _signInManager.ExternalLoginSignInAsync(
            loginProvider,
            providerKey,
            isPersistent: false,
            bypassTwoFactor: true);

        if (signInResult.Succeeded)
        {
            var user = await _userManager.FindByLoginAsync(loginProvider, providerKey);

            if (user != null && user.IsEnabled)
            {
                return await PrepareJwt(user);
            }
        }
        else
        {
            var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            var name = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.GivenName);
            var surname = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Surname);

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    PhoneNumber = "",
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    AuthenticatorKey = "",
                    AccessFailedCount = 0,
                    Email = email,
                    IsEnabled = true,
                    IsDeleted = false,
                    EmailConfirmed = true,
                    FirstName = name ?? "",
                    LastName = surname ?? ""
                };
            }

            var identityResult = await _userManager.CreateAsync(user);
            var identityRoleResult = await _userManager.AddToRoleAsync(user, ApplicationIdentityConstants.Roles.Member);

            if (identityResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, externalLoginInfo);
                return await PrepareJwt(user);
            }
            else
            {
                throw new AuthenticationException("User entity could not be created.");
            }
        }

        return null;
    }

    public (AuthenticationProperties, string) PrepareGoogleLoginProperties(string? returnUrl)
    {
        var provider = GoogleDefaults.AuthenticationScheme;
        var redirectUrl = $"/api/auth/google-callback?returnUrl={returnUrl}";
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        properties.AllowRefresh = true;

        return (properties, provider);
    }

    private async Task<bool> IsValidUser(string username, string password)
    {
        ApplicationUser user = await GetUserByUserName(username);

        if (user == null)
        {
            throw new AuthenticationException("Password or username was incorrect.");
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

    private string GenerateJwtToken(ApplicationUser user, string role)
    {
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

    private async Task<TokenResponse> PrepareJwt(ApplicationUser user)
    {
        var userDto = new UserEntity
        {
            IdentityId = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };
        string role = (await _userManager.GetRolesAsync(user))[0];
        string jwtToken = GenerateJwtToken(user, role);

        await _userManager.UpdateAsync(user);

        return new TokenResponse(userDto,
                                 role,
                                 jwtToken
                                 //""//refreshToken.Token
                                 );
    }

}
