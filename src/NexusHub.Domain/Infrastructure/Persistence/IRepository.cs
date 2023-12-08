using NexusHub.Domain.Infrastructure.Models;
using System.Linq.Expressions;

namespace NexusHub.Domain.Infrastructure.Persistence;

public interface IRepository<TEntity> where TEntity : AuditableEntity
{
    // Add
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Add(TEntity entity);
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
    IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

    // Read
    Task<TEntity>? GetByIdAsync(Guid id);
    TEntity? GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);
    IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression);

    // Update
    Task<TEntity> UpdateAsync(TEntity entity);
    TEntity Update(TEntity entity);
    Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities);
    IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);

    // Delete
    Task<TEntity> DeleteAsync(TEntity entity);
    TEntity Delete(TEntity entity);
    Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities);
    IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entities);
}