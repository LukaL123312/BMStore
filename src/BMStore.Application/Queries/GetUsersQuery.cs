using BMStore.Domain.Entities;
using MediatR;

namespace BMStore.Application.Queries;

public class GetUsersQuery : IRequest<IEnumerable<UserEntity>>
{
}
