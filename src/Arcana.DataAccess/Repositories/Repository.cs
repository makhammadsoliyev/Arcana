using Arcana.DataAccess.Context;
using Arcana.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Arcana.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly DbSet<TEntity> set;

    public Repository(ArcanaDbContext context)
    {
        set = context.Set<TEntity>();
    }

    public async Task<TEntity> DeleteAsync(TEntity entity) 
        => await Task.FromResult(set.Remove(entity).Entity);

    public async Task<TEntity> InsertAsync(TEntity entity)
        => (await set.AddAsync(entity)).Entity;

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null,
                                         string[] includes = null,
                                         bool isTracking = true)
    {
        var query = expression is not null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracking)
            query.AsNoTracking();

        return query;
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression,
                                           string[] includes = null)
    {
        var query = set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync()  ;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
        => await Task.FromResult(set.Update(entity).Entity);
}
