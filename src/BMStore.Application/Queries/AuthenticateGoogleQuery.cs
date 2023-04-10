using MediatR;

using Microsoft.AspNetCore.Authentication;

namespace BMStore.Application.Queries;

public record AuthenticateGoogleQuery(string? ReturnUrl) : IRequest<AuthenticateGoogleQueryResponse>;

public record AuthenticateGoogleQueryResponse(AuthenticationProperties Properties, string Provider);
