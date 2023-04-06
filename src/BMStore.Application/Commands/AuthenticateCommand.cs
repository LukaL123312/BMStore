using BMStore.Application.Models;

using FluentValidation;

using MediatR;

namespace BMStore.Application.Commands
{
    public class AuthenticateCommand
        : TokenRequest, IRequest<AuthenticateCommandResponse>
    {
    }

    public class AuthenticateCommandResponse
    {
        public TokenResponse Resource { get; set; }
    }

    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}
