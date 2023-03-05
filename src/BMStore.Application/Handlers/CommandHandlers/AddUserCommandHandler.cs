using BMStore.Application.Commands;
using BMStore.Application.Interfaces.IUnitOfWork;
using BMStore.Domain.Entities;
using FluentValidation;
using MediatR;

namespace BMStore.Application.Handlers.CommandHandlers;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddUserCommand> _validator;

    public AddUserCommandHandler(IUnitOfWork unitOfWork, IValidator<AddUserCommand> validator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            await _unitOfWork.UserRepository.AddAsync(new UserEntity
            {
                Name = request.Name,
                Surname = request.Surname,
                PhoneNumber = request.PhoneNumber,
                SubDomainName = request.SubDomainName,
                
            }, cancellationToken);

            return await _unitOfWork.SaveChangeAsync();
        }

        var failures = validationResult.Errors;

        throw new ValidationException(failures);
    }
}
