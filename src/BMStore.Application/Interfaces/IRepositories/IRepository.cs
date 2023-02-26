using System.Linq.Expressions;

namespace BMStore.Application.Interfaces.IRepositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, CancellationToken cancellationToken = default);
    void Remove(TEntity entity);
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}