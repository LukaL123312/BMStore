using BMStore.Application.Interfaces.IRepositories;

namespace BMStore.Application.Interfaces.IUnitOfWork;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    Task<int> SaveChangeAsync();
}
