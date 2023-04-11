using BMStore.Application.Commands;
using BMStore.Application.Interfaces;

using MediatR;

namespace BMStore.Application.Handlers.CommandHandlers
{
    public class AuthenticateGoogleCommandHandler : IRequestHandler<AuthenticateGoogleCommand, AuthenticateGoogleCommandResponse>
    {

        private readonly IAuthService _authService;

        public AuthenticateGoogleCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthenticateGoogleCommandResponse> Handle(AuthenticateGoogleCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.AuthenticateGoogleAsync(cancellationToken);

            return new AuthenticateGoogleCommandResponse(token);
        }
    }
}
