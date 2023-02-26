namespace BMStore.Application.Interfaces.IUnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangeAsync();
}
