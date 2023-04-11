using BMStore.Application.Interfaces;
using BMStore.Application.Queries;

using MediatR;

namespace BMStore.Application.Handlers.QueryHandlers;

public class AuthenticateGoogleQueryHandler : IRequestHandler<AuthenticateGoogleQuery, AuthenticateGoogleQueryResponse>
{
    private readonly IAuthService _authService;

    public AuthenticateGoogleQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<AuthenticateGoogleQueryResponse> Handle(AuthenticateGoogleQuery request, CancellationToken cancellationToken)
    {
        var result = _authService.PrepareGoogleLoginProperties(request.ReturnUrl);

        var response = new AuthenticateGoogleQueryResponse(result.Item1, result.Item2);

        return Task.FromResult(response);
    }
}
