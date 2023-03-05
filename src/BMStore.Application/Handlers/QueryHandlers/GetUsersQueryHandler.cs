using BMStore.Application.Interfaces.IRepositories;
using BMStore.Application.Queries;
using BMStore.Domain.Entities;
using MediatR;

namespace BMStore.Application.Handlers.QueryHandlers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserEntity>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserEntity>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var response = await _userRepository.GetAllAsync(cancellationToken);
        return response.ToList();
    }
}
