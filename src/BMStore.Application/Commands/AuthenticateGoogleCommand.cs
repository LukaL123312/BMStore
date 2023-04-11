using BMStore.Application.Models;

using MediatR;

namespace BMStore.Application.Commands;

public record AuthenticateGoogleCommand() : IRequest<AuthenticateGoogleCommandResponse>;
public record AuthenticateGoogleCommandResponse(TokenResponse Resource);
