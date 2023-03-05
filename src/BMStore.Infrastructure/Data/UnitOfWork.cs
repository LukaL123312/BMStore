using BMStore.Application.Interfaces.IRepositories;
using BMStore.Application.Interfaces.IUnitOfWork;
using BMStore.Infrastructure.Data.DbContext;
using BMStore.Infrastructure.Data.Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private BMStoreDbContext _context;

    public UnitOfWork(BMStoreDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUserRepository UserRepository => new UserRepository(_context);

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}