using BMStore.Application.Interfaces.IRepositories;
using BMStore.Domain.Entities;
using BMStore.Infrastructure.Data.DbContext;

namespace BMStore.Infrastructure.Data.Repository;

public class UserRepository : Repository<UserEntity>, IUserRepository
{
    public UserRepository(BMStoreDbContext context) : base(context)
    {

    }
}