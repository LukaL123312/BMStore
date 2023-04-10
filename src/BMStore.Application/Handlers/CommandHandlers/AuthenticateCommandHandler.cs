using BMStore.Application.Commands;
using BMStore.Application.CustomExceptions;
using BMStore.Application.Interfaces;
using BMStore.Application.Models;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace BMStore.Application.Handlers.CommandHandlers;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateCommandResponse>
{
    private readonly IAuthService _tokenService;
    private readonly HttpContext _httpContext;

    public AuthenticateCommandHandler(IAuthService tokenService,
                          IHttpContextAccessor httpContextAccessor)
    {
        this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        this._httpContext = (httpContextAccessor != null)
            ? httpContextAccessor.HttpContext
            : throw new ArgumentNullException(nameof(httpContextAccessor));

    }

    public async Task<AuthenticateCommandResponse> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
    {
        var response = new AuthenticateCommandResponse();

        string ipAddress = _httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

        TokenResponse tokenResponse = await _tokenService.Authenticate(command, ipAddress);

        response.Resource = tokenResponse ?? throw new InvalidCredentialsException();

        return response;
    }
}
