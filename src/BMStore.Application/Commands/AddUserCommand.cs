using FluentValidation;
using MediatR;

namespace BMStore.Application.Commands;

public class AddUserCommand : IRequest<int>
{
    public string Name { get; set; }

    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string SubDomainName { get; set; }
}

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.SubDomainName)
           .NotEmpty()
           .WithMessage("{PropertyName} is required.");
    }
}
