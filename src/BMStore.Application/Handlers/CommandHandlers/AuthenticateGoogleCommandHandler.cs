using BMStore.Application.Commands;
using BMStore.Application.Interfaces;

using MediatR;

namespace BMStore.Application.Handlers.CommandHandlers
{
    public class AuthenticateGoogleCommandHandler : IRequestHandler<AuthenticateGoogleCommand, AuthenticateGoogleCommandResponse>
    {

        private readonly IAuthService _tokenService;

        public AuthenticateGoogleCommandHandler(IAuthService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<AuthenticateGoogleCommandResponse> Handle(AuthenticateGoogleCommand request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.AuthenticateGoogle();

            return new AuthenticateGoogleCommandResponse(token);
        }
    }
}
