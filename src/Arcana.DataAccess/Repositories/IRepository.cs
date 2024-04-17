using Arcana.Domain.Commons;
using System.Linq.Expressions;

namespace Arcana.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression,
                              string[] includes = null);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null,
                                  string[] includes = null,
                                  bool isTracking = true);
}
