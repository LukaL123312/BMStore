using BMStore.Application.Interfaces.IRepositories;
using BMStore.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BMStore.Infrastructure.Data.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly BMStoreDbContext _dbContext;

    public Repository(BMStoreDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }
    public virtual async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await Task.Run(() => {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        });
        return entity;
    }
    public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(match, cancellationToken);
    }
    public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().Where(match).ToListAsync(cancellationToken);
    }

    public void Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public virtual async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

