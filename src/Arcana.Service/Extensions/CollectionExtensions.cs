using Arcana.Domain.Commons;
using Arcana.Service.Configurations;
using Arcana.Service.Exceptions;
using Arcana.Service.Helpers;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Arcana.Service.Extensions;

public static class CollectionExtensions
{
    public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> entities,
                                                       Filter filter) where TEntity : Auditable
    {
        var expression = entities.Expression;

        var parameter = Expression.Parameter(typeof(TEntity), "x");

        MemberExpression selector;
        try
        {
            selector = Expression.PropertyOrField(parameter, filter?.OrderBy ?? "Id");
        }
        catch
        {
            throw new ArgumentIsNotValidException("Specified property is not found");
        }

        var method = string.Equals(filter?.OrderType
                                   ?? "asc", "desc", StringComparison.OrdinalIgnoreCase)
                                ? "OrderByDescending" : "OrderBy";

        expression = Expression.Call(typeof(Queryable),
                                     method,
                                     [entities.ElementType, selector.Type],
                                     expression,
                                     Expression.Quote(Expression.Lambda(selector, parameter)));

        return entities.Provider.CreateQuery<TEntity>(expression);
    }

    public static IQueryable<TEntity> ToPaginate<TEntity>(this IQueryable<TEntity> entities,
                                                          PaginationParams @params) where TEntity : Auditable
    {
        var metaData = new PaginationMetaData(entities.Count(), @params);
        var json = JsonConvert.SerializeObject(metaData);
        var key = "X-Pagination";

        if (HttpContextHelper.ResponseHeaders is not null
            && HttpContextHelper.ResponseHeaders.ContainsKey(key))
            HttpContextHelper.ResponseHeaders.Remove(key);

        HttpContextHelper.ResponseHeaders?.Add(key, json);

        return @params.Index > 0 && @params.PageSize > 0 ?
            entities.Skip((@params.Index - 1) * @params.PageSize).Take(@params.PageSize) :
            throw new ArgumentIsNotValidException("Pagination parameters is not valid");
    }
}
